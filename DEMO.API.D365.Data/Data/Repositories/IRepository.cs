namespace DEMO.API.D365.Data.Data.Repositories
{
    using DEMO.API.D365.DataProvider.CrmContext;

    public interface IRepository<TEntity> where TEntity : ITrackedBusinessObject
    {
        Task<TEntity> GetByIdAsync(Guid id);
        IAsyncEnumerable<TEntity> GetByFilter(params KeyValuePair<string, object>[] filters);
        Task<Guid?> GetFirstReference(params KeyValuePair<string, object>[] filters);
        IAsyncEnumerable<TEntity> RetrieveMultipleByFetchAsync(string fetch, bool overrideColumns);
        IEnumerable<TEntity> RetrieveMultipleByFetch(string fetch, bool overrideColumns);
        Task<IEnumerable<TEntity>> GetAllActiveAsync();
        void Create(TEntity entity);
        void Attach(TEntity entity);
        void Delete(TEntity entity);
        Task<bool> SaveAsync();
    }
}
