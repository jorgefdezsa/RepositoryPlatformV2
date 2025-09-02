namespace DEMO.API.D365.Data.Data.Repositories.Masters
{
    using DEMO.API.D365.DataProvider.CrmContext;

    public interface IMasterEntityRepository<T> : IRepository<T> where T : ITrackedBusinessObject, new()
    {
        Task<T?> GetByCode(string code);
    }
}
