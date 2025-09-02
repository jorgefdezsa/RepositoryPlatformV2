namespace DEMO.API.SQL.Integrations.Repositories
{
    using DEMO.API.SQL.Integrations.Enums;
    using DEMO.API.SQL.Integrations.Models;

    public interface ILogIntegrationRepository
    {
        Task<IntegrationInputLog> CreateIntegrationInputLog(IntegrationCatalogEnum catalog, IntegrationTriggerEnum trigger, IntegrationOperationEnum operation, string? jsonRequest = null, string? jsonResponse = null, string? responseCode = null, string? responseBody = null);

        Task<IntegrationInputLog> CreateIntegrationInputLog(IntegrationCatalogEnum catalog, IntegrationTriggerEnum trigger, int jobNumber, string pageNumber, int NumRequests, string? jsonRequest = null, string? jsonResponse = null, string? responseCode = null, string? responseBody = null);

        Task<IntegrationInputLog> CreateIntegrationInputLog(IntegrationCatalogEnum catalog, IntegrationTriggerEnum trigger, IntegrationOperationEnum operation, int retryNumber, string jsonRequest, string jsonResponse, string responseCode, int? jobNumber = null, string pageNumber = "");

        Task<IntegrationOutputLog> CreateIntegrationOutputLog(IntegrationCatalogEnum catalog, IntegrationTriggerEnum trigger, int retryNumber, string jsonRequest, string jsonResponse, string responseCode, string endpoint);

        Task<DateTime?> GetLastIntegrationDate(IntegrationCatalogEnum catalog);

        Task<int?> GetLastJobNumber(IntegrationCatalogEnum catalog);
    }
}
