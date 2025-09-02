namespace DEMO.API.Functions.Common.Wrapper
{
    using DEMO.API.D365.Services.Common;
    using System.Net;
    public class ResponseMessage
    {
        public HttpStatusCode StatusCode
        {
            get; set;
        }
        public ResponseContent Content { get; set; }

        public OperationType Operation { get; set; }
    }
}
