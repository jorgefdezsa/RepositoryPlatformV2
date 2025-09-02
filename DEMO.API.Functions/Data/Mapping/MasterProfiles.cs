namespace DEMO.API.Functions.Data.Mapping
{
    using AutoMapper;
    using DEMO.API.Core.Domain.Entities;
    using DEMO.API.D365.Services.Masters.Data.Request;

    public class MastersProfiles : Profile
    {
        public MastersProfiles()
        {
            CreateMap<MasterEntityRequest, LegalEntity>()
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Description));
        
        }
    }
}
