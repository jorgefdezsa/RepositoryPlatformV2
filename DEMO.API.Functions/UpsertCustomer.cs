namespace DEMO.API.Functions
{
    using AutoMapper;
    using DEMO.API.Core.Exceptions;
    using DEMO.API.D365.Services.Customer;
    using DEMO.API.D365.Services.Customer.Data.Requests;
    using DEMO.API.D365.Services.Customer.Data.Responses;
    using DEMO.API.Functions.Common;
    using DEMO.API.Functions.Common.Wrapper;
    using DEMO.API.Functions.Extensions;
    using DEMO.API.Functions.Helpers.Types.ServiceBus;
    using DEMO.API.Functions.Helpers.Types.ServiceBus.Enums;
    using DEMO.API.Functions.Models;
    using DEMO.API.Functions.Presenters;
    using DEMO.API.Functions.Validators;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Azure.Functions.Worker.Http;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
    using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
    using Microsoft.Extensions.Logging;
    using System.Net;

    public class UpsertCustomer
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly IApiPresenter<UpsertCustomerResponse> _presenter;
        private readonly CommonFunctions _helperFunc;
        private readonly IRetryManager _retryManager;

        public UpsertCustomer(ILoggerFactory loggerFactory,
            IMapper mapper, CommonFunctions helperFunc,
            IRetryManager retryManager,
            ICustomerService customerService, IApiPresenter<UpsertCustomerResponse> presenter)
        {
            _logger = loggerFactory.CreateLogger<UpsertCustomer>();
            _mapper = mapper;
            _helperFunc = helperFunc;
            _customerService = customerService;
            _presenter = presenter;
            _retryManager = retryManager;

        }


        [OpenApiIgnore]
        [OpenApiOperation(operationId: "UpserCustomer", tags: new[] { "customer" }, Summary = "Customer", Description = "Upsert customer in Field Services.", Visibility = OpenApiVisibilityType.Important)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpsertCustomerModel), Description = "UpsertCustomer model", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ResponseContent), Summary = "The response", Description = "200. Sucessfull")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(ResponseContent), Summary = "The response", Description = "400. Bad Request")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Conflict, contentType: "application/json", bodyType: typeof(ResponseContent), Summary = "The response", Description = "409. Conflict")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(ResponseContent), Summary = "The response", Description = "500. Internal Server Error")]

        [Function("UpsertCustomer")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req, FunctionContext context)
        {
            var upsertCustomerValidatable = await req.GetJsonBody<UpsertCustomerModel, UpsertCustomerValidator>();
            var success = await UpsertCustomerInternal(context, upsertCustomerValidatable);
            var response = await _helperFunc.GetResponse(context, _presenter.ContentResult, true);

            if (!success)
                await _retryManager.SendMessage(upsertCustomerValidatable.RequestBody, "Upsertcustomer", DateTime.UtcNow, ScheduledMode.Lineal);

            return response;
        }

        [OpenApiIgnore]
        [Function("RetryUpsertCustomer")]
        public async Task RetryUpsertEmployee([ServiceBusTrigger("upsertcustomer", "upsertcustomer_subs", Connection = "ServiceBusConnection")] string message,
           FunctionContext context)
        {

            var upsertCustomerValidatable = message.ParseMessage<UpsertCustomerModel, UpsertCustomerValidator>();
            context.AddMessageSB(message, "upsertcustomer");
            var executionTimeStamp = context.GetCustomExecutionTimeStampMessage();
            var retryCount = context.GetCustomRetryCountMessage();
            var success = await UpsertCustomerInternal(context, upsertCustomerValidatable);
            context.AddResponseFunction(_presenter.ContentResult);

            if (!success)
                await _retryManager.ReSendMessage(upsertCustomerValidatable.RequestBody, "upsertcustomer", executionTimeStamp, retryCount, ScheduledMode.Lineal);
        }

        #region Private

        private async Task<bool> UpsertCustomerInternal(FunctionContext context, ValidatableRequest<UpsertCustomerModel> upsertCustomerValidatable)
        {
            if (upsertCustomerValidatable.IsValid)
            {
                var dtoUpsertCustomerRequest = _mapper.Map<UpsertCustomerRequest>(upsertCustomerValidatable.Value);
   
                return await _customerService.Handle(dtoUpsertCustomerRequest, _presenter);
            }
            else
            {
                throw new BadRequestException(upsertCustomerValidatable.Errors.Select(p => p.ErrorMessage).ToList());
            }
        }

        #endregion
    }
}
