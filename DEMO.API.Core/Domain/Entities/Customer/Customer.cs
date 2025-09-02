namespace DEMO.API.Core.Domain.Entities.Customer
{
    using AutoMapper.Configuration.Annotations;
    using DEMO.API.Core.Domain.Metadata.Contact;
    using DEMO.API.Core.Domain.Metadata.Customer;
    using DEMO.API.Core.Domain.Metadata.Masters;
    using DEMO.API.D365.DataProvider.Attributes;
    using DEMO.API.D365.DataProvider.CrmContext;

    public class Customer : BaseDomain, ITrackedBusinessObject
    {
        public static string LogicalName => AccountMetadata.EntityName;

        [SourceMember(AccountMetadata.PrimaryName)]
        public string Name { get; set; }

        public Guid? CrmId { get; set; }

        [SourceMember(AccountMetadata.CustomerERPID)]
        public string IdErp { get; set; }

        [SourceMember("address1_line1")]
        public string Address { get; set; }
        [SourceMember("address1_country")]
        public string Country { get; set; }

        [SourceMember("address1_city")] 
        public string City { get; set; }
        [SourceMember("address1_stateorprovince")] 
        public string State_Province { get; set; }

        [SourceMember("address1_postalcode")] 
        public string PostalCode { get; set; }

        [SourceMember(AccountMetadata.LegalEntity)]
        [D365_Lookup(LegalEntityMetadata.EntityName)]
        public Guid? LegalEntity { get; set; }

        [SourceMember(AccountMetadata.FiscalID)]
        public string FiscalId { get; set; }

        [SourceMember(AccountMetadata.PrimaryContact)]
        [D365_Lookup(ContactMetadata.EntityName)]
        public Guid? PrimaryContact { get; set; }

        [SourceMember(AccountMetadata.IntegrationTimeStamp)]
        public override DateTime? IntegrationTimeStamp { get; set; }

        [SourceMember(AccountMetadata.RelationshipType)]
        [D365_PickListAtribute]
        public AccountMetadata.RelationshipType_OptionSet? RelationShipType { get; set; }

        public void Merge(Customer target)
        {
            Address = target.Address;
            Country = target.Country;
            City = target.City;
            State_Province = target.State_Province;
            PostalCode = target.PostalCode;
            LegalEntity = target.LegalEntity;
            FiscalId = target.FiscalId;
            IntegrationTimeStamp = target.IntegrationTimeStamp;
            RelationShipType = target.RelationShipType;
        }
    }
}
