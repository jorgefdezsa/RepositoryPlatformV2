namespace DEMO.API.D365.Data.Data.Repositories.Masters
{
    using DEMO.API.D365.DataProvider.CrmContext;

    public class MasterEntityRepository<T> : Repository<T>, IMasterEntityRepository<T> where T : ITrackedBusinessObject, new()
    {
        private readonly ICrmContext _context;

        public MasterEntityRepository(ICrmContext context) : base(context)
        {
            _context = context;
        }

        public async Task<T?> GetByCode(string code)
        {
            return await _context.RetrieveMultipleByAttributesAsync<T>(new KeyValuePair<string, object>("jfs_code", code)).FirstOrDefaultAsync();
        }
    }
}
