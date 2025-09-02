namespace DEMO.API.Functions.Data.Mapping
{
    using AutoMapper;
    using DEMO.API.D365.Services.Customer.Data.Requests;
    using DEMO.API.Functions.Models;

    public class ExampleFunctionProfile : Profile
    {
        public ExampleFunctionProfile()
        {
            CreateMap<UpsertCustomerModel, UpsertCustomerRequest>()
                .ForMember(dest => dest.IdErp, opt => opt.MapFrom(src => src.CustomerId));
        }
    }
}
