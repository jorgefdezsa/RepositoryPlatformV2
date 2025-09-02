namespace DEMO.API.SQL.Integrations.Models
{
    public partial class IntegrationOutputLog
    {
        public int Id { get; set; }

        public int IntegrationCatalogId { get; set; }

        public int IntegrationTriggerId { get; set; }

        public DateTime IntegrationDate { get; set; }

        public int? RetryNumber { get; set; }

        public string? ResponseCode { get; set; }

        public string? JsonRequest { get; set; }

        public string? JsonResponse { get; set; }

        public string? ExternalEndpoint { get; set; }

        public virtual IntegrationCatalog IntegrationCatalog { get; set; } = null!;

        public virtual IntegrationTrigger IntegrationTrigger { get; set; } = null!;
    }
}
