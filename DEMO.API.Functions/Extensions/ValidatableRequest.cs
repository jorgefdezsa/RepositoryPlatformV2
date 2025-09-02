namespace DEMO.API.Functions.Extensions
{
    using FluentValidation.Results;

    public class ValidatableRequest<T>
    {
        /// <summary>
        /// The deserialized value of the request.
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// Whether or not the deserialized value was found to be valid.
        /// </summary>
        public bool IsValid { get; set; }

        public string RequestBody { get; set; }

        /// <summary>
        /// The collection of validation errors.
        /// </summary>
        public IList<ValidationFailure> Errors { get; set; }
    }
}
