namespace DEMO.API.D365.Data.Data.Repositories.Customer
{
    using DEMO.API.Core.Domain.Entities.Customer;
    using DEMO.API.Core.Domain.Metadata.Customer;
    using DEMO.API.D365.DataProvider.CrmContext;

    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly ICrmContext _context;
        public CustomerRepository(ICrmContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Customer> GetCustomerByErpId(string IdErp)
        {
            var parameters = new KeyValuePair<string, object>[] { new KeyValuePair<string, object>(AccountMetadata.IdErp, IdErp) };
            return await GetByFilter(parameters).FirstOrDefaultAsync();
        }
    }
}
