namespace DEMO.API.Functions.Extensions
{
    using FluentValidation;
    using Newtonsoft.Json;

    public static class StringExtensions
    {
        public static ValidatableRequest<T> ParseMessage<T, V>(this string requestBody)
           where V : AbstractValidator<T>, new()
        {
            var requestObject = JsonConvert.DeserializeObject<T>(requestBody);
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

            return new ValidatableRequest<T>
            {
                Value = requestObject,
                RequestBody = requestBody,
                IsValid = true
            };
        }
    }
}
