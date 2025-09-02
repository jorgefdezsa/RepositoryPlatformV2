namespace DEMO.API.D365.DataProvider.CrmContext
{
    using DEMO.API.D365.DataProvider.Collection;
    using Microsoft.Crm.Sdk.Messages;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Query;
    using System.Collections;

    public partial class CrmContext : ICrmContext
    {
        public CrmContext()
        {
        }

        public async Task<OrganizationResponse> Execute(OrganizationRequest request)
        {
            var crmService = connectionProvider.GetDataverseConnection();
            return await crmService.ExecuteAsync(request);
        }

        public void Detach(string logicalname = "", Guid? guid = null)
        {
            if (String.IsNullOrWhiteSpace(logicalname))
                trackedCache.Clear();
            else
            {
                List<string> keys = new List<string>();
                var strstart = logicalname.ToLowerInvariant() + "|";

                if (guid == null)
                {
                    foreach (var key in trackedCache.Keys)
                    {
                        if (key.StartsWith(strstart))
                            keys.Add(key);

                    }
                }
                else
                {
                    var strend = "|" + guid?.ToString().ToLowerInvariant()!;
                    foreach (var key in trackedCache.Keys)
                    {
                        if (key.StartsWith(strstart) && key.EndsWith(strend))
                            keys.Add(key);

                    }
                }
                if (keys.Any())
                    foreach (var key in keys)
                        trackedCache.Remove(key, out _);

            }

        }

        public void Detach(ITrackedBusinessObject business)
        {
            if (business != null)
                foreach (var o in trackedCache)
                {
                    if (o.Value.Business == business)
                    {
                        trackedCache.Remove(o.Key, out _);
                        break;
                    }
                }
        }
        public void Detach(Entity entity)
        {
            if (entity != null)
                foreach (var o in trackedCache)
                {
                    if (o.Value.Entity == entity)
                    {
                        trackedCache.Remove(o.Key, out _);
                        break;
                    }
                }
        }

        public async Task<T> GetByIdAsync<T>(Guid id) where T : ITrackedBusinessObject, new()
        {
            var connection = connectionProvider.GetDataverseConnection();

            var entity = await connection.RetrieveAsync(T.LogicalName, id, GetColumnSet<T>());
            return MapAndAttach<T>(entity);
        }

        private static ColumnSet GetColumnSet<T>() where T : ITrackedBusinessObject, new()
        {
            return new Microsoft.Xrm.Sdk.Query.ColumnSet(GetOrCreatePropertyList<T>().Select(x => x.Item2.ToLowerInvariant()).ToArray());
        }

        public Task<T> FindByIdAsync<T>(Guid id) where T : ITrackedBusinessObject, new()
        {
            if (trackedCache.TryGetValue(GetKey(id, T.LogicalName), out CrmTrackedCacheItem value) && value?.Business != null && value?.Business is T returnvalue)
            {

                return Task.FromResult(returnvalue);
            }
            return GetByIdAsync<T>(id);
        }

        public Task<bool> SaveAllChangesAsync()
        {
            if (ReadOnlyContext)
                throw new InvalidOperationException("Read only context");
            return SaveAllChangesAsyncInternal(5);
        }
        public async Task<bool> SaveAllChangesAsyncInternal(int maxdepth) 
        {
            if (maxdepth == 0)
            {
                throw new Exception("Cyclic reference found");
            }
            var anychange = false;
            var crmService = connectionProvider.GetDataverseConnection();
            foreach (var tracked in trackedCache.ToList())
            {
                var key = tracked.Key;
                var entity = tracked.Value.Entity;
                var business = tracked.Value.Business;

                if (business == null && entity != null)
                {
                    // delete
                    await crmService.DeleteAsync(entity.LogicalName, entity.Id);
                    anychange = true;
                    trackedCache.TryRemove(key, out _);
                }
                else if (entity == null)
                {
                    throw new Exception("Business entity is null and it should never be");
                }
                else
                {


                    if (entity.Id == Guid.Empty)
                    {
                        var newEntity = (Entity)tracked.Value.MapperB2E.Map(business, business.GetType(), typeof(Entity), (a) => a.Items.Add("ICrmContext", this));
                        newEntity.Id = business.CrmId ?? Guid.Empty;
                        if (CompareEntities(newEntity, entity))
                        {
                            var guid = await crmService.CreateAsync(newEntity);
                            anychange = true;
                            entity.Id = guid;
                            trackedCache.TryRemove(key, out _);
                            tracked.Value.Entity = entity;
                            tracked.Value.MapperE2B.Map(entity, business, typeof(Entity), business.GetType(), (a) => a.Items.Add("ICrmContext", this));
                            trackedCache.AddOrUpdate(GetKey(entity), (a) => tracked.Value, (a, _) => tracked.Value);

                        }

                    }
                    else
                    {

                        var previousAttributes = entity.Attributes.ToDictionary(x => x.Key, x => x.Value);
                        var newEntity = (Entity)tracked.Value.MapperB2E.Map(business, business.GetType(), typeof(Entity), (a) => a.Items.Add("ICrmContext", this));

                        try
                        {
                            if (CompareEntities(newEntity, entity))
                            {
                                await crmService.UpdateAsync(newEntity);
                                anychange = true;
                            }
                        }
                        catch
                        {
                            entity.Attributes.Clear();
                            foreach (var c in previousAttributes)
                                entity.Attributes.Add(c.Key, c.Value);
                            throw;
                        }
                    }
                    if (updateRelationships(tracked.Value))
                        anychange = true;
                }
            }


            if (anychange)
                await SaveAllChangesAsyncInternal(maxdepth - 1);
            return anychange;
        }

        private bool updateRelationships(CrmTrackedCacheItem tracked)
        {
            var anychanges = false;
            var relationships = GetOrCreateOneToNRelations(tracked.Business.GetType());
            foreach (var relationship in relationships)
            {
                var list = relationship.Item1.GetValue(tracked.Business);
                if (list != null && list is not TrackableCollectionBase && list is ICollection listc && listc.Count > 0)
                {
                    var newlist = relationship.Item2.CreateCollection(this, relationship.Item1, tracked.Business.CrmId.Value, listc);
                    relationship.Item1.SetValue(tracked.Business, newlist);
                    anychanges = true;

                }
                else if (list is TrackableCollectionBase x)
                {
                    anychanges = anychanges || x.UpdateParendTrackedId(tracked.Business.CrmId.Value);
                }
            }
            return anychanges;
        }

        public void Attach<T>(T business) where T : ITrackedBusinessObject, new()
        {
            var mapper = GetBusinessToEntityMapper<T>();

            Entity e = new Entity(T.LogicalName, business.CrmId ?? Guid.Empty);

            Track(business, e);
        }

        public void Insert<T>(T business) where T : ITrackedBusinessObject, new()
        {
            var entity = new Entity(T.LogicalName, business.CrmId ?? Guid.Empty);
            entity.Id = Guid.Empty;
            Track(business, entity);
        }


        public void Delete<T>(T business) where T : ITrackedBusinessObject, new()
        {
            string k = null;
            if (business.CrmId.HasValue)
            {
                k = GetKey(business.CrmId.Value, T.LogicalName);
            }
            else
            {
                k = trackedCache.FirstOrDefault(x => x.Value.Business.Equals(business)).Key;
            }
            if (k != null)
            {
                if (trackedCache.TryGetValue(k, out var n))
                {
                    n.Business = null;

                }
            }

        }

        public IEnumerable<T> RetrieveMultipleByAttributes<T>(params KeyValuePair<string, object>[] filters) where T : ITrackedBusinessObject, new()
        {
            var crmService = connectionProvider.GetDataverseConnection();
            QueryByAttribute q = new QueryByAttribute(T.LogicalName);
            q.ColumnSet = GetColumnSet<T>();
            foreach (var filtro in filters)
                q.AddAttributeValue(filtro.Key, filtro.Value);
            q.PageInfo = new PagingInfo();
            q.PageInfo.PageNumber = 1;
            EntityCollection result;
            do
            {
                result = crmService.RetrieveMultiple(q);

                foreach (var t in result.Entities)
                    yield return MapAndAttach<T>(t);
                q.PageInfo.PagingCookie = result.PagingCookie;
                q.PageInfo.PageNumber++;

            } while (result.MoreRecords);

        }

        public async IAsyncEnumerable<T> RetrieveMultipleByAttributesAsync<T>(params KeyValuePair<string, object>[] filters) where T : ITrackedBusinessObject, new()
        {
            var crmService = connectionProvider.GetDataverseConnection();
            QueryByAttribute q = new QueryByAttribute(T.LogicalName);
            q.ColumnSet = GetColumnSet<T>();
            foreach (var filtro in filters)
                q.AddAttributeValue(filtro.Key, filtro.Value);
            q.PageInfo = new PagingInfo();
            q.PageInfo.PageNumber = 1;
            EntityCollection result;
            do
            {
                result = await crmService.RetrieveMultipleAsync(q);
                foreach (var t in result.Entities)
                    yield return MapAndAttach<T>(t);
                q.PageInfo.PagingCookie = result.PagingCookie;
                q.PageInfo.PageNumber++;

            } while (result.MoreRecords);

        }

        public IEnumerable<T> RetrieveMultipleByFetch<T>(string fetch, bool overwriteColumns = true) where T : ITrackedBusinessObject, new()
        {
            var crmService = connectionProvider.GetDataverseConnection();
            var moreRecords = false;
            int fetchCount = 5000;
            int pageNumber = 1;

            var fetchExpression = new FetchExpression(fetch);

            FetchXmlToQueryExpressionRequest fetchXmlToQueryExpressionRequest = new FetchXmlToQueryExpressionRequest()
            {
                FetchXml = fetchExpression.Query
            };

            FetchXmlToQueryExpressionResponse fetchXmlToQueryExpressionResponse = (crmService.Execute(fetchXmlToQueryExpressionRequest) as FetchXmlToQueryExpressionResponse);
            var queryExpression = fetchXmlToQueryExpressionResponse.Query;
            if (overwriteColumns)
                queryExpression.ColumnSet = GetColumnSet<T>();
            queryExpression.PageInfo = new PagingInfo();
            queryExpression.PageInfo.PageNumber = 1;
            EntityCollection result;
            do
            {
                result = crmService.RetrieveMultiple(queryExpression);
                foreach (var t in result.Entities)
                    yield return MapAndAttach<T>(t);
                queryExpression.PageInfo.PagingCookie = result.PagingCookie;
                queryExpression.PageInfo.PageNumber++;

            } while (moreRecords);
        }

        /// <summary>
        /// Get only Reference of Record with minimum columns and without attach T in cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filters"></param>
        /// <returns></returns>
        public async Task<Guid?> GetFirstReference<T>(params KeyValuePair<string, object>[] filters) where T : ITrackedBusinessObject, new()
        {
            var crmService = connectionProvider.GetDataverseConnection();
            QueryByAttribute q = new QueryByAttribute(T.LogicalName);
            //q.ColumnSet = GetColumnSet<T>();
            foreach (var filtro in filters)
                q.AddAttributeValue(filtro.Key, filtro.Value);
            var result = await crmService.RetrieveMultipleAsync(q);
            if (result.Entities.Count > 0)
                return result.Entities[0].Id;
            else
                return null;
        }

        public async IAsyncEnumerable<T> RetrieveMultipleByFetchAsync<T>(string fetch, bool overwriteColumns = true) where T : ITrackedBusinessObject, new()
        {
            var crmService = connectionProvider.GetDataverseConnection();
            var moreRecords = false;
            int fetchCount = 5000;
            int pageNumber = 1;

            var fetchExpression = new FetchExpression(fetch);

            FetchXmlToQueryExpressionRequest fetchXmlToQueryExpressionRequest = new FetchXmlToQueryExpressionRequest()
            {
                FetchXml = fetchExpression.Query
            };

            FetchXmlToQueryExpressionResponse fetchXmlToQueryExpressionResponse = (crmService.Execute(fetchXmlToQueryExpressionRequest) as FetchXmlToQueryExpressionResponse);
            var queryExpression = fetchXmlToQueryExpressionResponse.Query;
            if (overwriteColumns)
                queryExpression.ColumnSet = GetColumnSet<T>();
            queryExpression.PageInfo = new PagingInfo();
            queryExpression.PageInfo.PageNumber = 1;
            EntityCollection result;
            do
            {
                result = await crmService.RetrieveMultipleAsync(queryExpression);
                foreach (var t in result.Entities)
                    yield return MapAndAttach<T>(t);
                queryExpression.PageInfo.PagingCookie = result.PagingCookie;
                queryExpression.PageInfo.PageNumber++;

            } while (moreRecords);
        }

        #region Private Methods

        //private string CreateXml(string xml, string cookie, int page, int count)
        //{
        //    StringReader stringReader = new StringReader(xml);
        //    XmlTextReader reader = new XmlTextReader(stringReader);

        //    // Load document
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(reader);

        //    return CreateXml(doc, cookie, page, count);
        //}

        //private string CreateXml(XmlDocument doc, string cookie, int page, int count)
        //{
        //    XmlAttributeCollection attrs = doc.DocumentElement.Attributes;

        //    if (cookie != null)
        //    {
        //        XmlAttribute pagingAttr = doc.CreateAttribute("paging-cookie");
        //        pagingAttr.Value = cookie;
        //        attrs.Append(pagingAttr);
        //    }

        //    XmlAttribute pageAttr = doc.CreateAttribute("page");
        //    pageAttr.Value = System.Convert.ToString(page);
        //    attrs.Append(pageAttr);

        //    XmlAttribute countAttr = doc.CreateAttribute("count");
        //    countAttr.Value = System.Convert.ToString(count);
        //    attrs.Append(countAttr);

        //    StringBuilder sb = new StringBuilder(1024);
        //    StringWriter stringWriter = new StringWriter(sb);

        //    using (XmlTextWriter writer = new XmlTextWriter(stringWriter))
        //    {
        //        doc.WriteTo(writer);
        //        //writer.Close();
        //    }

        //    return sb.ToString();
        //}

        #endregion
    }
}
