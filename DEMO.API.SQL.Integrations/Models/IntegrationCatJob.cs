namespace DEMO.API.SQL.Integrations.Models
{
    public partial class IntegrationCatJob
    {
        public int Id { get; set; }

        public int IntegrationCatalogId { get; set; }

        public int JobNumber { get; set; }

        public DateTime ChangeDate { get; set; }

        public virtual IntegrationCatalog IntegrationCatalog { get; set; } = null!;
    }
}
