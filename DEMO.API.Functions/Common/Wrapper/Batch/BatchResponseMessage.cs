namespace DEMO.API.Functions.Common.Wrapper.Batch
{
    using System.Net;

    public class BatchResponseMessage
    {
        public HttpStatusCode StatusCode
        {
            get; set;
        }
        public List<BatchResponseContent> Content { get; set; }

    }
}
