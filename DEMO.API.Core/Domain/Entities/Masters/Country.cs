namespace DEMO.API.Core.Domain.Entities.Masters
{
    using AutoMapper.Configuration.Annotations;
    using DEMO.API.Core.Domain.Metadata.Masters;
    using DEMO.API.D365.DataProvider.CrmContext;

    public class Country : BaseDomain, ITrackedBusinessObject
    {
        public static string LogicalName => CountryMetadata.EntityName;

        public Guid? CrmId { get; set; }

        [SourceMember(CountryMetadata.PrimaryName)]
        public string Name { get; set; }

        [SourceMember(CountryMetadata.ISOCode)]
        public string Code { get; set; }
        [Ignore]
        public override DateTime? IntegrationTimeStamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
