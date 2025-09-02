namespace DEMO.API.D365.Services.Masters.Data.Request
{
    using DEMO.API.D365.Services.Common;
    using DEMO.API.D365.Services.Masters.Data.Response;

    public class MasterEntityRequest : BaseServiceRequestMessage, IServiceRequest<MasterEntityResponse>
    {
        public string Code { get; set; }
        public string Description { get; set; }

    }
}
