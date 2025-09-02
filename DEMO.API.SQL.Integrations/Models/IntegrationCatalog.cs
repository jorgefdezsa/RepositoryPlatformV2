namespace DEMO.API.SQL.Integrations.Models
{
    public partial class IntegrationCatalog
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Path { get; set; } = null!;

        public int IntegrationSystemId { get; set; }

        public int IntegrationProcessId { get; set; }

        public int IntegrationTypeId { get; set; }

        public int? TopPage { get; set; }

        public bool Active { get; set; }

        public virtual ICollection<IntegrationCatJob> IntegrationCatJobs { get; set; } = new List<IntegrationCatJob>();

        public virtual ICollection<IntegrationInputLog> IntegrationInputLogs { get; set; } = new List<IntegrationInputLog>();

        public virtual ICollection<IntegrationOutputLog> IntegrationOutputLogs { get; set; } = new List<IntegrationOutputLog>();

        public virtual IntegrationProcess IntegrationProcess { get; set; } = null!;

        public virtual IntegrationSystem IntegrationSystem { get; set; } = null!;

        public virtual IntegrationType IntegrationType { get; set; } = null!;
    }
}
