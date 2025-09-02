namespace DEMO.API.Core.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException() { }
        public BadRequestException(string message) : base(message) { }


        public BadRequestException(List<string> validationErrors)
            : base($"Errores detectados: {string.Join(", ", validationErrors.ToArray())}")
        {
        }
    }
}
