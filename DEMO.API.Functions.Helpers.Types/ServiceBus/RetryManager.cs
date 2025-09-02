namespace DEMO.API.Functions.Helpers.Types.ServiceBus
{
    using Azure.Messaging.ServiceBus;
    using Azure.Messaging.ServiceBus.Administration;
    using DEMO.API.Functions.Helpers.Types.Config.Retry;
    using DEMO.API.Functions.Helpers.Types.Extensions;
    using DEMO.API.Functions.Helpers.Types.ServiceBus.Enums;
    using Microsoft.Extensions.Azure;
    using Microsoft.Extensions.Options;
    using System.Collections.Concurrent;
    using static DEMO.API.Resources.ProxyMessages;

    public class RetryManager : IRetryManager
    {
        private readonly ServiceBusClient _clientSB;
        private readonly ServiceBusAdministrationClient _clientAdminSB;
        #region Constructor

        private readonly IOptions<RetryManagerOptions> _retryOptions;
        private static readonly ConcurrentDictionary<string, ServiceBusSender> senders = new ConcurrentDictionary<string, ServiceBusSender>();

        public RetryManager(IAzureClientFactory<ServiceBusClient> serviceBusClientFactory,
            IAzureClientFactory<ServiceBusAdministrationClient> serviceBusAdminFactory,
            IOptions<RetryManagerOptions> retryOptions)
        {

            _retryOptions = retryOptions;
            _clientSB = serviceBusClientFactory.CreateClient("sbDefault");
            _clientAdminSB = serviceBusAdminFactory.CreateClient("sbAdminDefault");
        }

        #endregion

        #region IRetryManager

        public async Task ReSendMessage(string message, string messageType, DateTime? timestamp, int retryCount,
            ScheduledMode mode)
        {
            retryCount++;
            var sender = GetOrAddServiceBusSender(messageType);

            if (retryCount < _retryOptions.Value.MaxRetryCount)
            {
                var sbMessage = new ServiceBusMessage(message);
                sbMessage.ApplicationProperties.Add("executionTimeStamp", timestamp ?? DateTime.UtcNow);
                sbMessage.ApplicationProperties.Add("retryCount", retryCount);
                await sender.ScheduleMessageAsync(sbMessage, ScheduleNextExecutionTime(retryCount, mode));
            }
            else
            {
                var sbMessageToDealLetterExpirationTime = new ServiceBusMessage(message);
                sbMessageToDealLetterExpirationTime.ApplicationProperties.Add("executionTimeStamp", timestamp ?? DateTime.UtcNow);
                sbMessageToDealLetterExpirationTime.ApplicationProperties.Add("retryCount", retryCount);
                sbMessageToDealLetterExpirationTime.TimeToLive = TimeSpan.FromMilliseconds(10);
                await sender.SendMessageAsync(sbMessageToDealLetterExpirationTime);
            }

        }

        public async Task SendMessage(string message, string messageType, DateTime timestamp, ScheduledMode mode)
        {
            var sender = GetOrAddServiceBusSender(messageType);
            var sbMessage = new ServiceBusMessage(message);
            sbMessage.ApplicationProperties.Add("executionTimeStamp", timestamp);
            sbMessage.ApplicationProperties.Add("RetryCount", 0);
            await sender.ScheduleMessageAsync(sbMessage, ScheduleNextExecutionTime(0, mode));
        }

        public async Task<IEnumerable<Tuple<bool, string, string>>> SendBatchMessages(IEnumerable<Tuple<string, string>> messages, string messageType,
            int jobNumber, int curentPage)
        {
            var responses = new List<Tuple<bool, string, string>>();
            if (messages != null && messages.Count() > 0)
            {
                List<ServiceBusMessage> serviceBusMessages = new();
                var sender = GetOrAddServiceBusSender(messageType);
                var serviceBusMessageBatch = await sender.CreateMessageBatchAsync();
                List<Tuple<string, string>> pendingSent = new List<Tuple<string, string>>();
                int totalCount = messages.Count();
                int count = 0;
                foreach (var item in messages)
                {
                    bool last = (count == totalCount - 1);
                    var sbMessage = new ServiceBusMessage(item.Item2);
                    sbMessage.ApplicationProperties.Add("RetryCount", 0);
                    sbMessage.ApplicationProperties.Add("JobNumber", jobNumber);
                    sbMessage.ApplicationProperties.Add("CurrentPage", curentPage);
                    var added = serviceBusMessageBatch.TryAddMessage(sbMessage);
                    if (added)
                        pendingSent.Add(item);
                    else if (serviceBusMessageBatch.Count == 0)
                        responses.Add(new Tuple<bool, string, string>(false, item.Item1, GetMessageKey(MessagesConstants.MESSAGE_SERVICEBUS_NOTFIT)));

                    if ((!added || last) && serviceBusMessageBatch.Count > 0) 
                    {
                        try
                        {
                            await SendBatchMessage(serviceBusMessageBatch, messageType);
     
                            if (pendingSent.Count > 0)
                            {
                                pendingSent.ForEach(sent => responses.Add(new Tuple<bool, string, string>(true, item.Item1, string.Empty)));
                                pendingSent.Clear();
                            }
                        }
                        catch (Exception ex)
                        {
                            if (pendingSent.Count > 0)
                            {
                                pendingSent.ForEach(sent => responses.Add(new Tuple<bool, string, string>(false, item.Item1, string.Format(GetMessageKey(MessagesConstants.MESSAGE_SERVICEBUS_NOTFIT), messageType, ex.Message.Truncate(100)))));
                                pendingSent.Clear();
                            }
                        }
                    }
                    count++;
                }
            }

            return responses;
        }

        #endregion

        #region Private

        private async Task<bool> AddOrSendBatchMessages(ServiceBusMessageBatch batchMessage, ServiceBusMessage message, string messageType)
        {
            var added = batchMessage.TryAddMessage(message);
            if (!added && batchMessage.Count > 0)
            {
                await SendBatchMessage(batchMessage, messageType);
                return await AddOrSendBatchMessages(batchMessage, message, messageType);
            }
            else if (added)
                return true;
            else
                return false;
        }

        private async Task SendBatchMessage(ServiceBusMessageBatch batchMessage, string messageType)
        {
            if (batchMessage.Count > 0)
            {
                var sender = GetOrAddServiceBusSender(messageType);
                await sender.SendMessagesAsync(batchMessage);
                batchMessage.Dispose();
            }
        }

        private DateTimeOffset ScheduleNextExecutionTime(int RetryCount, ScheduledMode mode)
        {
            switch (mode)
            {
                case ScheduledMode.Exponential:
                    return DateTime.UtcNow.AddHours(RetryCount == 0 ? 1 : RetryCount);
                    break;
                case ScheduledMode.Lineal:
                    return DateTime.UtcNow.AddMinutes(_retryOptions.Value.ScheduleIntervalLinealRetryInMinutes);
                    break;
                case ScheduledMode.FromTransientError:
                    return DateTime.UtcNow.AddMinutes(_retryOptions.Value.ScheduleIntervalTransientErrorsRetryInMinutes);
                    break;
                case ScheduledMode.Instant:
                    return DateTime.UtcNow.AddSeconds(10);
                    break;
                default: //Aplicamos el retry Mode Lineal
                    return DateTime.UtcNow.AddMinutes(_retryOptions.Value.ScheduleIntervalLinealRetryInMinutes);
                    break;
            }
        }

        private ServiceBusSender GetOrAddServiceBusSender(string messageType)
        {
            return senders.GetOrAdd(messageType,
                (t) =>
                {
                    var existsTopic = _clientAdminSB.TopicExistsAsync(messageType.ToLower()).GetAwaiter().GetResult();
                    if (!existsTopic)
                    {
                        CreateTopicOptions optionsTopic = new CreateTopicOptions(messageType.ToLower())
                        {
                            MaxSizeInMegabytes = _retryOptions.Value.TopicMaxSizeInMegabytes,
                            DefaultMessageTimeToLive = TimeSpan.FromDays(_retryOptions.Value.DefaultTopicMessageTimeToLive)
                        };
                        _clientAdminSB.CreateTopicAsync(optionsTopic);
                    }
                    return _clientSB.CreateSender(messageType.ToLower());
                });
        }


        #endregion
    }
}
