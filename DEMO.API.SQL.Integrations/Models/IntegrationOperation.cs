namespace DEMO.API.SQL.Integrations.Models
{
    public partial class IntegrationOperation
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool? Active { get; set; }

        public virtual ICollection<IntegrationInputLog> IntegrationInputLogs { get; set; } = new List<IntegrationInputLog>();
    }
}
