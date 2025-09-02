namespace DEMO.API.Functions.Models
{
    public class UpsertCustomerModel
    {
        public string CustomerId { get; set; }
        public string FiscalId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State_Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string LegalEntityId { get; set; }
        public DateTime IntegrationTimeStamp { get; set; }
        public int? RelationshipType { get; set; }
    }
}
