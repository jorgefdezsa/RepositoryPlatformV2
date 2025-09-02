namespace DEMO.API.D365.DataProvider.CrmContext
{
    using AutoMapper;
    using DEMO.API.D365.DataProvider.Attributes;
    using DEMO.API.D365.DataProvider.Collection;
    using DEMO.API.D365.DataProvider.CrmConnectionProvider;
    using Microsoft.Extensions.Logging;
    using Microsoft.Xrm.Sdk;
    using System.Collections.Concurrent;
    using System.Reflection;

    public partial class CrmContext : ICrmContext, IDisposable
    {
        #region Static caches
        private static readonly ConcurrentDictionary<Type, Tuple<PropertyInfo, string, D365_AtributeBase>[]> typePropertiesCache = new ConcurrentDictionary<Type, Tuple<PropertyInfo, string, D365_AtributeBase>[]>();
        private static readonly ConcurrentDictionary<Type, IMapper> entityToBusinessMapperCache = new ConcurrentDictionary<Type, IMapper>();
        private static readonly ConcurrentDictionary<Type, IMapper> businessToEntityMapperCache = new ConcurrentDictionary<Type, IMapper>();
        private static readonly ConcurrentDictionary<Type, Tuple<PropertyInfo, TrackedForeingKeyAttributeBase>[]> onetonrelations = new ConcurrentDictionary<Type, Tuple<PropertyInfo, TrackedForeingKeyAttributeBase>[]>();

        #endregion
        private readonly ConcurrentDictionary<string, CrmTrackedCacheItem> trackedCache;
        private readonly ICrmConnectionsProvider connectionProvider;
        private readonly ILogger<CrmContext> logger;
        private bool disposedValue;

        public bool ReadOnlyContext { get; private set; } = false;

        public CrmContext(ICrmConnectionsProvider connectionProvider, ILogger<CrmContext> logger)
        {
            this.connectionProvider = connectionProvider ?? throw new ArgumentNullException(nameof(connectionProvider));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.trackedCache = new ConcurrentDictionary<string, CrmTrackedCacheItem>();
        }

        public void SetAsReadonly()
        {
            this.ReadOnlyContext = true;
        }

        private T MapAndAttach<T>(Entity entity, bool onlyMap = false) where T : ITrackedBusinessObject, new()
        {
            bool isViewObject = (typeof(T).GetInterface(nameof(ITrackedViewObject)) != null);

            if (!isViewObject && T.LogicalName != entity.LogicalName)
                throw new Exception($"La clase {typeof(T).Name} no se puede mapear con una entidad de tipo {entity.LogicalName}");
            var mapper = GetEntityToBusinessMapper<T>();

            var result = mapper.Map<T>(entity, (a) => a.Items.Add("ICrmContext", this));
            if (ReadOnlyContext || isViewObject || onlyMap)
                return result;
            var key = GetKey(entity);
            var cacheitem = new CrmTrackedCacheItem(entity, result, mapper, GetBusinessToEntityMapper<T>());
            trackedCache.AddOrUpdate(key, cacheitem, (k, t) => cacheitem);
            return result;
        }

        private string GetKey(Entity entity)
        {
            if (entity.Id == Guid.Empty)
                return entity.LogicalName.ToLowerInvariant() + "|new_" + Guid.NewGuid().ToString().ToLowerInvariant();
            return entity.LogicalName.ToLowerInvariant() + "|" + entity.Id.ToString().ToLowerInvariant();

        }

        private string GetKey(Guid id, string logicalname)
        {
            if (id == Guid.Empty)
                return logicalname.ToLowerInvariant() + "|new_" + Guid.NewGuid().ToString().ToLowerInvariant();
            return logicalname.ToLowerInvariant() + "|" + id.ToString().ToLowerInvariant();

        }

        private bool CompareEntities(Entity newEntity, Entity oldEntity)
        {
            var changes = false;
            newEntity.RowVersion = oldEntity.RowVersion;
            foreach (var i in newEntity.Attributes.ToList())
            {
                if (AreEqual(oldEntity, i))
                    newEntity.Attributes.Remove(i.Key);
                else
                {
                    //if (i.Value != null) 
                    changes = true;
                    if (oldEntity.Attributes.Contains(i.Key))
                        oldEntity.Attributes[i.Key] = i.Value;
                    else
                        oldEntity.Attributes.Add(i.Key, i.Value);
                }
            }
            return changes;
        }

        private bool AreEqual(Entity oldEntity, KeyValuePair<string, object> newVal)
        {
            if (oldEntity.Attributes.TryGetValue(newVal.Key, out var old))
            {
                if (old == null && newVal.Value == null)
                    return true;
                else if (old != null && newVal.Value != null)
                {
                    return AreEqualType(old, newVal.Value);
                }
                else
                    return false; 
            }
            else if (newVal.Value != null)
                return false;
            else
                return true;
        }

        private bool AreEqualType(object oldValue, object newValue)
        {
            if (oldValue is EntityReference old_er && newValue is EntityReference new_er && old_er.LogicalName == new_er.LogicalName
                        && old_er.Id.Equals(old_er.Id))
                return true;
            else if (oldValue is OptionSetValue old_opt && newValue is OptionSetValue new_opt && old_opt.Value == new_opt.Value)
                return true;
            else if (oldValue is Money old_mn && newValue is Money new_mn && old_mn.Value == new_mn.Value)
                return true;
            else if (oldValue is DateTime old_date && newValue is DateTime new_date)
            {
                TimeSpan diff = new_date - old_date;
                if (Math.Abs(Math.Floor(diff.TotalSeconds)) > 0)
                    return false;
                else
                    return true;
            }
            else return oldValue.Equals(newValue);
        }

        public TrackableCollection<P, H> GetFixedCollectionFor<P, H>(string ParentClassPropertyName, IEnumerable<H> currentlist, Guid? parentid = null)
            where P : ITrackedBusinessObject
            where H : ITrackedBusinessObject, new()
        {
            var props = GetOrCreateOneToNRelations<P>();
            var prop = props.First(x => x.Item1.Name == ParentClassPropertyName);
            var c = new TrackableCollection<P, H>(this, prop.Item1, prop.Item2.GetParentReferenceColumn(), parentid ?? Guid.Empty, true);
            foreach (H h in currentlist)
                c.Add(h);
            return c;
        }

        private void Track<T>(T business, Entity entity) where T : ITrackedBusinessObject, new()
        {


            var key = GetKey(entity);
            var cacheitem = new CrmTrackedCacheItem(entity, business, GetEntityToBusinessMapper<T>(), GetBusinessToEntityMapper<T>());
            if (!ReadOnlyContext)
                trackedCache.AddOrUpdate(key, cacheitem, (k, t) =>
                {
                    if (cacheitem.Business != business as ITrackedBusinessObject)
                        throw new Exception("Business entity was already tracked by other object");
                    return cacheitem;
                });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.trackedCache.Clear();
                }

                disposedValue = true;
            }
        }


        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
