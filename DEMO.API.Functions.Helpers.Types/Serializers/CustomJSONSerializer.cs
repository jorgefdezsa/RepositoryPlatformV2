namespace DEMO.API.Functions.Helpers.Types.Serializers
{
    using System.Text.Json;

    public class CustomJSONSerializer
    {
        public static string Serialize(object value)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            return JsonSerializer.Serialize(value, options);
        }

        public static T Deserialize<T>(string value)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<T>(value, options);
        }
    }
}
