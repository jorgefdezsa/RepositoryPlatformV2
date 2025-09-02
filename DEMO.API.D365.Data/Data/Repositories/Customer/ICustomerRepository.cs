namespace DEMO.API.D365.Data.Data.Repositories.Customer
{
    using DEMO.API.Core.Domain.Entities.Customer;
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerByErpId(string IdErp);
    }
}
