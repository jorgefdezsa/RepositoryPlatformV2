namespace DEMO.API.D365.DataProvider.CrmContext
{
    using DEMO.API.D365.DataProvider.Collection;
    using Microsoft.Xrm.Sdk;

    public interface ICrmContext : IDisposable
    {
        void SetAsReadonly();
        void Attach<T>(T business) where T : ITrackedBusinessObject, new();

        Task<OrganizationResponse> Execute(OrganizationRequest request);

        Task<T> GetByIdAsync<T>(Guid id) where T : ITrackedBusinessObject, new();
        Task<bool> SaveAllChangesAsync();
        void Detach(string logicalname = "", Guid? guid = null);
        void Detach(Entity entity);
        void Detach(ITrackedBusinessObject business);
        void Insert<T>(T business) where T : ITrackedBusinessObject, new();
        Task<T> FindByIdAsync<T>(Guid id) where T : ITrackedBusinessObject, new();
        Task<Guid?> GetFirstReference<T>(params KeyValuePair<string, object>[] filters) where T : ITrackedBusinessObject, new();
        IAsyncEnumerable<T> RetrieveMultipleByAttributesAsync<T>(params KeyValuePair<string, object>[] filters) where T : ITrackedBusinessObject, new();
        IAsyncEnumerable<T> RetrieveMultipleByFetchAsync<T>(string fetch, bool overwriteColumns) where T : ITrackedBusinessObject, new();

        IEnumerable<T> RetrieveMultipleByAttributes<T>(params KeyValuePair<string, object>[] filters) where T : ITrackedBusinessObject, new();
        IEnumerable<T> RetrieveMultipleByFetch<T>(string fetch, bool overwriteColumns) where T : ITrackedBusinessObject, new();
        void Delete<T>(T business) where T : ITrackedBusinessObject, new();
        TrackableCollection<P, H> GetFixedCollectionFor<P, H>(string ParentClassPropertyName, IEnumerable<H> currentlist, Guid? parentid = null)
            where P : ITrackedBusinessObject
            where H : ITrackedBusinessObject, new();
    }
}
