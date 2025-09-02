namespace DEMO.API.Functions.Presenters
{
    using DEMO.API.D365.Services.Common;
    using DEMO.API.Functions.Common.Wrapper;
    using System.Net;

    public class GenericResponsePresenter : IApiPresenter<BaseServiceResponseMessage>
    {

        public ResponseMessage ContentResult { get; private set; }

        public GenericResponsePresenter()
        {
            ContentResult = new ResponseMessage();
        }

        public async Task<bool> Handle(BaseServiceResponseMessage serviceResponse)
        {
            ContentResult.StatusCode = (serviceResponse.Success ? HttpStatusCode.OK : HttpStatusCode.Conflict);
            ContentResult.Content = new ResponseContent()
            {
                Errors = serviceResponse.Errors,
                Success = serviceResponse.Success,
                Message = (serviceResponse.Success ? "Request successful" : "Incorrect request:")
            };
            ContentResult.Operation = serviceResponse.Operation;
            return true;
        }
    }
}
