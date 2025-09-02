namespace DEMO.API.D365.Services.Customer
{
    using DEMO.API.D365.Services.Common;
    using DEMO.API.D365.Services.Customer.Data.Requests;
    using DEMO.API.D365.Services.Customer.Data.Responses;

    public interface ICustomerService
    {
        Task<bool> Handle(UpsertCustomerRequest message, IPresenter<UpsertCustomerResponse> presenter);
    }
}
