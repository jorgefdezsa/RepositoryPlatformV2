namespace DEMO.API.Functions.Extensions
{
    using FluentValidation;
    using Microsoft.Azure.Functions.Worker.Http;
    using System.Text.Json;

    public static class HttpRequestExtensions
    {

        public static async Task<ValidatableRequest<T>> GetJsonBody<T, V>(this HttpRequestData request)
            where V : AbstractValidator<T>, new()
        {
            var requestBody = await request.ReadAsStringAsync();
            var requestObject = await request.GetJsonBody<T>(requestBody);
            var validator = new V();
            var validationResult = validator.Validate(requestObject);

            if (!validationResult.IsValid)
            {
                return new ValidatableRequest<T>
                {
                    Value = requestObject,
                    IsValid = false,
                    RequestBody = requestBody,
                    Errors = validationResult.Errors
                };
            }
            //request.Body.Position = 0; 
            return new ValidatableRequest<T>
            {
                Value = requestObject,
                RequestBody = requestBody,
                IsValid = true
            };
        }

        public static async Task<T> GetJsonBody<T>(this HttpRequestData request)
        {
            var requestBody = await request.ReadAsStringAsync();
            request.Body.Position = 0;
            return await request.GetJsonBody<T>(requestBody);
        }

        public static bool IsBatchPath(this HttpRequestData request)
        {
            return request.Url.AbsolutePath.Contains("batch", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsSwaggerPath(this HttpRequestData request)
        {
            return request.Url.AbsolutePath.Contains("swagger", StringComparison.InvariantCultureIgnoreCase);
        }

        public static bool IsOpenApiPath(this HttpRequestData request)
        {
            return request.Url.AbsolutePath.Contains("openapi", StringComparison.InvariantCultureIgnoreCase);
        }

        #region Private Methods

        private static async Task<T> GetJsonBody<T>(this HttpRequestData request, string requestBody)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<T>(requestBody, options);
        }

        #endregion
    }
}
