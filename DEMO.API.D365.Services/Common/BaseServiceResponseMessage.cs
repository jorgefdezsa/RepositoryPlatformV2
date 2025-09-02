namespace DEMO.API.D365.Services.Common
{
    public enum OperationType
    {
        Create = 1,
        Read = 2,
        Update = 3,
        Delete = 4,
    }

    public class BaseServiceResponseMessage
    {
        public OperationType Operation { get; }
        public bool Success { get; }
        public string Message { get; }
        public IEnumerable<Error> Errors { get; }


        public BaseServiceResponseMessage(OperationType operation, bool success = false, string message = null)
        {
            Success = success;
            Message = message;
            Operation = operation;
        }

        public BaseServiceResponseMessage(OperationType operation, IEnumerable<Error> errors, bool success = false, string message = null)
        {
            Success = success;
            Message = message;
            Errors = errors;
            Operation = operation;
        }
    }
}
