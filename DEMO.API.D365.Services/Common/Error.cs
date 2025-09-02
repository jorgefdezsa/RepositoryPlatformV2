namespace DEMO.API.D365.Services.Common
{
    using DEMO.API.D365.Services.Enums;

    public sealed class Error
    {
        public int Code { get; }
        public string Description { get; }

        public Error(ErrorCodes code, string description)
        {
            Code = (int)code;
            Description = description;
        }
    }
}
