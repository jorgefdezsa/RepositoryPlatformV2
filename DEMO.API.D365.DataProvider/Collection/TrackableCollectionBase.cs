namespace DEMO.API.D365.DataProvider.Collection
{
    using AutoMapper.Configuration.Annotations;
    using DEMO.API.D365.DataProvider.CrmContext;
    using System.Collections;
    using System.Reflection;

    public abstract class TrackableCollectionBase
    {
        internal abstract bool UpdateParendTrackedId(Guid value);

    }
    public class TrackableCollection<P, H> : TrackableCollectionBase, ICollection<H>
        where H : ITrackedBusinessObject, new()
        where P : ITrackedBusinessObject
    {
        private readonly ICrmContext _crmContext;
        private readonly PropertyInfo property;
        private readonly string parentReferenceColumnName;
        private readonly Guid parentid;
        private readonly PropertyInfo parentReferenceProperty;
        public List<H> baseList = new List<H>();
        public bool downloaded = false;



        public TrackableCollection(ICrmContext crmContext, PropertyInfo property, string parentreferencecolumnname, Guid id, bool alreadydownloaded = false)
        {
            this._crmContext = crmContext;
            this.property = property;
            this.parentReferenceColumnName = parentreferencecolumnname;
            this.parentid = id;
            this.downloaded = alreadydownloaded;
            this.parentReferenceProperty = typeof(H)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .First(x => x.Name.Equals(parentreferencecolumnname, StringComparison.InvariantCultureIgnoreCase)
             || (x.GetCustomAttribute<SourceMemberAttribute>()?.Name.Equals(parentreferencecolumnname, StringComparison.InvariantCultureIgnoreCase) ?? false));
        }

        public int Count
        {
            get
            {
                checkDownloaded();
                return baseList.Count();
            }
        }

        private void checkDownloaded()
        {
            if (!downloaded)
                lock (this)
                {
                    if (!downloaded)
                    {
                        foreach (var hijo in _crmContext.RetrieveMultipleByAttributes<H>(new KeyValuePair<string, object>(parentReferenceColumnName, parentid)))
                        {
                            var existente = baseList.FirstOrDefault(x => x.CrmId == hijo.CrmId);
                            if (existente != null)
                                baseList.Remove(existente);
                            baseList.Add(hijo);

                        }
                        downloaded = true;
                    }
                }
        }

        public bool IsReadOnly => false;



        public void Add(H item)
        {
            baseList.Add(item);

            _crmContext.Attach(item);
            parentReferenceProperty.SetValue(item, parentid);
        }



        public void Clear()
        {
            checkDownloaded();
            foreach (var item in baseList)
            {
                parentReferenceProperty.SetValue(item, null);
            }
        }

        public bool Contains(H item)
        {
            if (baseList.Contains(item)) return true;
            checkDownloaded();
            return baseList.Contains(item);
        }

        public void CopyTo(H[] array, int arrayIndex)
        {
            checkDownloaded();
            baseList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<H> GetEnumerator()
        {
            checkDownloaded();
            return baseList.GetEnumerator();
        }

        public bool Remove(H item)
        {
            if (baseList.Contains(item))
            {
                parentReferenceProperty.SetValue(item, null);
                baseList.Remove(item);
                return true;
            }
            else
            {
                checkDownloaded();
                if (baseList.Contains(item))
                {
                    baseList.Remove(item);
                    parentReferenceProperty.SetValue(item, null);
                    return true;
                }
                else
                    return false;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            checkDownloaded();
            return baseList.GetEnumerator();
        }

        internal override bool UpdateParendTrackedId(Guid value)
        {
            var changes = false;
            foreach (var x in baseList)
                if (parentReferenceProperty.GetValue(x) is not Guid g || g != value)
                {
                    parentReferenceProperty.SetValue(x, value);
                    changes = true;
                }
            return changes;
        }
    }
}
