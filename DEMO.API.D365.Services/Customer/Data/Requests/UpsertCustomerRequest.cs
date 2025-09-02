namespace DEMO.API.D365.Services.Customer.Data.Requests
{
    using DEMO.API.D365.Services.Common;
    using DEMO.API.D365.Services.Customer.Data.Responses;

    public class UpsertCustomerRequest : BaseServiceRequestMessage, IServiceRequest<UpsertCustomerResponse>
    {
        public string Name { get; set; }

        public string IdErp { get; set; }
        public string FiscalId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State_Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string LegalEntityId { get; set; }
        public int? RelationshipType { get; set; }
    }
}
