namespace DEMO.API.D365.Data.Data.Repositories
{
    using DEMO.API.D365.DataProvider.CrmContext;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : ITrackedBusinessObject, new()
    {

        private readonly ICrmContext _context;
        #region Constructores
        public Repository(ICrmContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var result = await _context.GetByIdAsync<TEntity>(id);
            return result;
        }

        public void Create(TEntity entity)
        {
            _context.Insert(entity);
        }

        public void Attach(TEntity entity)
        {
            _context.Attach(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Delete(entity);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveAllChangesAsync();
        }

        public IAsyncEnumerable<TEntity> GetByFilter(params KeyValuePair<string, object>[] filters)
        {
            return _context.RetrieveMultipleByAttributesAsync<TEntity>(filters);
        }

        public async Task<IEnumerable<TEntity>> GetAllActiveAsync()
        {
            KeyValuePair<string, object> filter = new KeyValuePair<string, object>("statecode", 0);
            var results = _context.RetrieveMultipleByAttributesAsync<TEntity>(filter);
            if (results != null)
            {
                return await results.ToListAsync();
            }
            else
            {
                return null;
            }
        }

        public async Task<Guid?> GetFirstReference(params KeyValuePair<string, object>[] filters)
        {
            return await _context.GetFirstReference<TEntity>(filters);
        }

        public IEnumerable<TEntity> RetrieveMultipleByFetch(string fetch, bool overwriteColumns = true)
        {
            return _context.RetrieveMultipleByFetch<TEntity>(fetch, overwriteColumns);
        }

        public IAsyncEnumerable<TEntity> RetrieveMultipleByFetchAsync(string fetch, bool overwriteColumns = true)
        {
            return _context.RetrieveMultipleByFetchAsync<TEntity>(fetch, overwriteColumns);
        }
    }
}
