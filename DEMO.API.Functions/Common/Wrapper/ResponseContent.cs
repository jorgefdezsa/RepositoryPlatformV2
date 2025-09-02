namespace DEMO.API.Functions.Common.Wrapper
{
    using System.Text.Json.Serialization;
    using DEMO.API.D365.Services.Common;

    public class ResponseContent
    {
        public ResponseContent()
        {
            this.Errors = new List<Error>();
            this.Message = string.Empty;
        }

        [JsonPropertyName("Success")]
        public bool Success { get; set; }
        [JsonPropertyName("Message")]
        public string Message { get; set; }
        public IEnumerable<Error> Errors { get; set; }
    }
}
