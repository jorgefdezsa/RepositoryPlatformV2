namespace DEMO.API.SQL.Integrations.Models
{
    public partial class IntegrationType
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool? Active { get; set; }

        public virtual ICollection<IntegrationCatalog> IntegrationCatalogs { get; set; } = new List<IntegrationCatalog>();
    }
}
