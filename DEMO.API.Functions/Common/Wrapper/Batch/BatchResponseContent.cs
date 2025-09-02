namespace DEMO.API.Functions.Common.Wrapper.Batch
{
    using System.Text.Json.Serialization;

    public class BatchResponseContent
    {
        public BatchResponseContent()
        {
            Message = string.Empty;
        }

        [JsonPropertyName("ResponseCode")]
        public int ResponseCode { get; set; }
        [JsonPropertyName("RequestIndex")]
        public string RequestIndex { get; set; }
        [JsonPropertyName("Message")]
        public string Message { get; set; }
    }
}
