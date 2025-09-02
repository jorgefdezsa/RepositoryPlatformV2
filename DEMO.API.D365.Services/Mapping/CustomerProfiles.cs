namespace DEMO.API.D365.Services.Mapping
{
    using AutoMapper;
    using DEMO.API.Core.Domain.Entities.Customer;
    using DEMO.API.D365.Services.Customer.Data.Requests;

    public class CustomerProfiles : Profile
    {
        public CustomerProfiles()
        {
            CreateMap<UpsertCustomerRequest, Customer>();
        }
    }
}
