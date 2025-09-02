namespace DEMO.API.Core.Domain.Entities
{
    using AutoMapper.Configuration.Annotations;
    using DEMO.API.D365.DataProvider.Attributes;

    public abstract class BaseDomain
    {
        [SourceMember("createdon")]
        public DateTime CreateOn { get; set; }
        [SourceMember("modifiedon")]
        public DateTime ModifiedOn { get; set; }
        [D365_PickListAtribute]
        [SourceMember("statecode")]
        public virtual int? State { get; set; }
        [D365_PickListAtribute]
        [SourceMember("statuscode")]
        public virtual int? StatusCode { get; set; }

        public abstract DateTime? IntegrationTimeStamp { get; set; }

        public virtual bool CanIntegrate(DateTime? messageIntegrationDate)
        {
            if (IntegrationTimeStamp.HasValue && messageIntegrationDate.HasValue)
                return IntegrationTimeStamp.Value.ToUniversalTime() < messageIntegrationDate.Value.ToUniversalTime();

            return true;
        }
    }
}
