namespace DEMO.API.Functions.Helpers.Types.Config
{
    public record AppConfiguration
    {
        public string SQLConnectionString { get; init; } = "";
        public string CRMConnectionString { get; init; } = "";
    }
}
