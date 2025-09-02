namespace DEMO.API.Functions.Common
{
    using DEMO.API.D365.Services.Common;
    using DEMO.API.Functions.Common.Wrapper;
    using DEMO.API.Functions.Common.Wrapper.Batch;
    using DEMO.API.Functions.Extensions;
    using DEMO.API.Functions.Helpers.Types.Serializers;
    using Microsoft.Azure.Functions.Worker;
    using Microsoft.Azure.Functions.Worker.Http;

    public class CommonFunctions
    {

        public CommonFunctions()
        {
        }
        public async Task<HttpResponseData> GetResponse(FunctionContext context, ResponseMessage message, bool showErrors)
        {
            var requestData = await context.GetHttpRequestDataAsync();
            if (requestData != null)
            {
                var response = requestData.CreateResponse(message.StatusCode);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                if (showErrors && message.Content != null && message.Content.Errors != null &&
                    message.Content.Errors.Any())
                    message.Content.Message += " " + ConcatenateErrors(message.Content.Errors);
                await response.WriteStringAsync(CustomJSONSerializer.Serialize(message.Content));
                context.AddResponseFunction(message);
                return response;
            }
            else
                return null;
        }

        public async Task<HttpResponseData> GetResponse(FunctionContext context, BatchResponseMessage message, bool showErrors)
        {
            var requestData = await context.GetHttpRequestDataAsync();
            if (requestData != null)
            {
                var response = requestData.CreateResponse(message.StatusCode);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                await response.WriteStringAsync(CustomJSONSerializer.Serialize(message.Content));
                context.AddResponseFunction(message);
                return response;
            }
            else
                return null;
        }

        #region Private
        private string ConcatenateErrors(IEnumerable<Error> errores)
        {
            return String.Join(',', errores.Select(p => $"{p.Code}: {p.Description}").ToArray());
        }
        #endregion
    }
}
