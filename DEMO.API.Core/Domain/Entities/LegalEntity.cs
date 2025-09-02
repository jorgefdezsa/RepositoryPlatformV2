namespace DEMO.API.Core.Domain.Entities
{
    using AutoMapper.Configuration.Annotations;
    using DEMO.API.Core.Domain.Metadata.Masters;
    using DEMO.API.D365.DataProvider.CrmContext;

    public class LegalEntity : BaseDomain, ITrackedBusinessObject
    {
        public static string LogicalName => LegalEntityMetadata.EntityName;

        public Guid? CrmId { get; set; }

        [SourceMember(LegalEntityMetadata.Code)]
        public string Code { get; set; }

        [SourceMember(LegalEntityMetadata.PrimaryName)]
        public string Name { get; set; }

        [SourceMember(LegalEntityMetadata.IntegrationTimeStamp)]
        public override DateTime? IntegrationTimeStamp { get; set; }
    }
}
