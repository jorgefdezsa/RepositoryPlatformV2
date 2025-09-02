namespace DEMO.API.Functions.Helpers.Types.ServiceBus
{
    using DEMO.API.Functions.Helpers.Types.ServiceBus.Enums;

    public interface IRetryManager
    {
        Task SendMessage(string message, string messageType, DateTime timestamp, ScheduledMode mode);

        Task ReSendMessage(string message, string messageType, DateTime? timestamp, int DeliveryCount, ScheduledMode mode);

        Task<IEnumerable<Tuple<bool, string, string>>> SendBatchMessages(IEnumerable<Tuple<string, string>> messages,
            string messageType, int jobNumber, int currentPage);
    }
}
