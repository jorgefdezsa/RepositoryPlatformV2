namespace DEMO.API.Functions.Helpers.Types.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this String value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}
