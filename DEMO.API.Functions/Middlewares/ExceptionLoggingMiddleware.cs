namespace DEMO.API.Functions.Middlewares
{
    using DEMO.API.Core.Exceptions;
    using DEMO.API.D365.Services.Common;
    using DEMO.API.D365.Services.Enums;
    using DEMO.API.Functions.Common;
    using DEMO.API.Functions.Common.Wrapper;
    using DEMO.API.Functions.Common.Wrapper.Batch;
    using DEMO.API.Functions.Extensions;
    using DEMO.API.Functions.Helpers.Types.Config;
    using DEMO.API.Functions.Helpers.Types.Extensions;
    using DEMO.API.Functions.Helpers.Types.Serializers;
    using DEMO.API.Functions.Helpers.Types.ServiceBus;
    using DEMO.API.Functions.Helpers.Types.ServiceBus.Enums;
    using DEMO.API.SQL.Integrations.Enums;
    using DEMO.API.SQL.Integrations.Repositories;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Azure.Functions.Worker.Http;
    using Microsoft.Azure.Functions.Worker.Middleware;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System.Net;

    public class ExceptionLoggingMiddleware : IFunctionsWorkerMiddleware
    {
        private readonly CommonFunctions _helperFunc;
        private readonly IRetryManager _retryManager;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IOptions<ProcessOptions> _processOptions;

        public ExceptionLoggingMiddleware(CommonFunctions helperFunc, IRetryManager retryManager,
            IServiceScopeFactory serviceScopeFactory, IOptions<ProcessOptions> processOptions)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _helperFunc = helperFunc;
            _retryManager = retryManager;
            _processOptions = processOptions;
            _exceptionHandlers = new Dictionary<Type, Func<FunctionContext, Exception, Task>>
                {
                    {
                        typeof(BadRequestException), HandleBadRequestException
                    }
                };
        }
        private readonly IDictionary<Type, Func<FunctionContext, Exception, Task>> _exceptionHandlers;
        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            try
            {
                var logger = context.GetLogger<ExceptionLoggingMiddleware>();
                logger.LogInformation($"Init Request: {context.FunctionDefinition.Name}");

                //Code before function execution here
                await next(context);
                // Code after function execution here
                logger.LogInformation($"End Rquest OK: {context.FunctionDefinition.Name}");

            }
            catch (AggregateException ae)
            {
                var logger = context.GetLogger<ExceptionLoggingMiddleware>();
                foreach (var innerException in ae.Flatten().InnerExceptions)
                    logger.LogError(innerException, $" Origin: {context.FunctionDefinition.Name}");

                if (ae.InnerException != null)
                {
                    logger.LogInformation(ae.InnerException, $"End request KO. Handling Exception..{context.FunctionDefinition.Name}");
                    await HandleExceptionAsync(context, ae.InnerException);
                }
            }
            catch (Exception ex)
            {
                var logger = context.GetLogger<ExceptionLoggingMiddleware>();
                logger.LogCritical(ex, $" Origin: {context.FunctionDefinition.Name}");
                await HandleExceptionAsync(context, ex);
                logger.LogInformation(ex, $"End request KO. Handling Exception..{context.FunctionDefinition.Name}");
            }
            finally
            {
                if (_processOptions.Value.LogToSql.HasValue && _processOptions.Value.LogToSql.Value)
                    await SaveIntegrationLog(context);
            }
        }

        private async Task SaveIntegrationLog(FunctionContext context)
        {

            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _logRepository = scope.ServiceProvider.GetService<ILogIntegrationRepository>();

                    if (context.IsHttpTriggerFunction())
                    {
                        var requestData = await context.GetHttpRequestDataAsync();
                        var requestBody = requestData?.ReadAsStringAsync();
                        if (requestData != null && !requestData.IsSwaggerPath() &&
                        !requestData.IsOpenApiPath())
                        {
                            if (!requestData.IsBatchPath())
                            {
                                if (context.Items.TryGetValue("responseFunction", out var _responseFunction))
                                {
                                    Enum.TryParse(context.FunctionDefinition.Name.ToString(), out IntegrationCatalogEnum catalog);
                                    IntegrationTriggerEnum trigger = IntegrationTriggerEnum.HttpTrigger;
                                    var operationType = ((ResponseMessage)_responseFunction).Operation;
                                    Enum.TryParse(operationType.ToString(), out IntegrationOperationEnum crudOperation);
                                    var responseMessage = (ResponseMessage)_responseFunction;
                                    var jsonResponse = CustomJSONSerializer.Serialize(responseMessage.Content);
                                    var statuscode = (int)responseMessage.StatusCode;
                                    await _logRepository.CreateIntegrationInputLog(catalog, trigger, crudOperation, requestBody?.Result?.ToString(),
                                    jsonResponse, statuscode.ToString(), null);
                                }
                            }
                            else
                            {
                                if (context.Items.TryGetValue("responseBatchFunction", out var _responseFunction))
                                {
                                    Enum.TryParse(context.FunctionDefinition.Name.ToString(), out IntegrationCatalogEnum catalog);
                                    IntegrationTriggerEnum trigger = IntegrationTriggerEnum.HttpTrigger;
                                    var batchInfo = context.GetBatchInfo();
                                    int jobNumber = 0;
                                    int NumItems = 0;
                                    string pageNumber = string.Empty;
                                    if (batchInfo != null && batchInfo.ContainsKey("JobNumber"))
                                        jobNumber = Convert.ToInt32(batchInfo["JobNumber"]);
                                    if (batchInfo != null && batchInfo.ContainsKey("NumItems"))
                                        NumItems = Convert.ToInt32(batchInfo["NumItems"]);
                                    if (batchInfo != null && batchInfo.ContainsKey("CurrentPage") && batchInfo.ContainsKey("TotalPages"))
                                    {
                                        pageNumber = $"{batchInfo["CurrentPage"]}//{batchInfo["CurrentPage"]}";
                                    }
                                    var responseMessage = (BatchResponseMessage)_responseFunction;
                                    var jsonResponse = CustomJSONSerializer.Serialize(responseMessage.Content);
                                    var statuscode = (int)responseMessage.StatusCode;
                                    await _logRepository.CreateIntegrationInputLog(catalog, trigger, jobNumber, pageNumber, NumItems, requestBody?.Result?.ToString().Truncate(2000),
                                        jsonResponse.Truncate(2000), statuscode.ToString(), null);
                                }
                            }
                        }
                    }
                    else if (context.IsServiceBusTriggerFunction())
                    {
                        if (context.Items.TryGetValue("messageBody", out var _messageBody) &&
                            context.Items.TryGetValue("responseFunction", out var _responseFunction))
                        {
                            var retryCount = context.GetCustomRetryCountMessage();
                            var jobNumber = context.GetCustomJobNumberMessage();
                            var currentPage = context.GetCustomCurrentPageMessage();
                            Enum.TryParse(context.FunctionDefinition.Name.ToString(), out IntegrationCatalogEnum catalog);
                            IntegrationTriggerEnum trigger = IntegrationTriggerEnum.ServiceBusTrigger;
                            var operationType = ((ResponseMessage)_responseFunction).Operation;
                            Enum.TryParse(operationType.ToString(), out IntegrationOperationEnum crudOperation);
                            var responseMessage = (ResponseMessage)_responseFunction;
                            var jsonResponse = CustomJSONSerializer.Serialize(responseMessage.Content);
                            var statuscode = (int)responseMessage.StatusCode;
                            await _logRepository.CreateIntegrationInputLog(catalog, trigger, crudOperation,
                                retryCount + 1, _messageBody.ToString(), jsonResponse, statuscode.ToString(), jobNumber, currentPage.ToString());
                        }
                    }
                }
            }
            catch
            {

            } 

        }

        #region Handle Exceptions

        private async Task HandleExceptionAsync(FunctionContext context, Exception ex)
        {
            if (_exceptionHandlers.ContainsKey(ex.GetType()))
            {
                await _exceptionHandlers[ex.GetType()].Invoke(context, ex);
            }
            else
            {
                await HandleUnknownException(context, ex);
            }

            if (ex is not BadRequestException)
            {
                if (context.IsHttpTriggerFunction())
                {
                    var requestData = await context.GetHttpRequestDataAsync();
                    if (!requestData.IsBatchPath())
                    {
                        var requestBody = await requestData.ReadAsStringAsync();
                        requestData.Body.Position = 0;
                        if (requestBody != null)
                            await _retryManager.SendMessage(requestBody, context.FunctionDefinition.Name, DateTime.UtcNow, ScheduledMode.FromTransientError);
                    }
                }
                else if (context.IsServiceBusTriggerFunction() && context.Items.TryGetValue("messageBody", out var _messageBody))
                {
                    var executionTimeStamp = context.GetCustomExecutionTimeStampMessage();
                    var retryCount = context.GetCustomRetryCountMessage();
                    var topic = context.GetTopic();
                    if (!string.IsNullOrEmpty(topic))
                        await _retryManager.ReSendMessage(_messageBody.ToString(), topic, executionTimeStamp, retryCount, ScheduledMode.FromTransientError);
                }
            }
        }

        private async Task SetResponse(FunctionContext context, ResponseMessage message)
        {
            if (context.IsHttpTriggerFunction())
            {
                var response = await _helperFunc.GetResponse(context, message, true);
                context.GetInvocationResult().Value = response;
            }
            else if (context.IsServiceBusTriggerFunction())
            {
                context.AddResponseFunction(message);
            }
        }

        private async Task HandleBadRequestException(FunctionContext context, Exception ex)
        {
            List<Error> errors = new List<Error>();
            errors.Add(new Error(ErrorCodes.BadRequest, ex.Message));

            var message = new ResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new ResponseContent()
                {
                    Success = false,
                    Message = "BAD REQUEST. Fail Validation",
                    Errors = errors
                }
            };

            await SetResponse(context, message);

        }

        private async Task HandleUnknownException(FunctionContext context, Exception ex)
        {
            List<Error> errors = new List<Error>
            {
                new Error(ErrorCodes.InternalError, ex.Message)
            };

            var message = new ResponseMessage()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new ResponseContent()
                {
                    Success = false,
                    Message = "INTERNAL UNKNOWN ERROR",
                    Errors = errors
                }
            };

            await SetResponse(context, message);
        }


        #endregion



    }
}
