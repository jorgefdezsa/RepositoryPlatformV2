namespace DEMO.API.D365.DataProvider.Collection
{
    using DEMO.API.D365.DataProvider.CrmContext;
    using System.Collections;
    using System.Reflection;

    public abstract class TrackedForeingKeyAttributeBase : Attribute
    {
        public abstract string GetParentReferenceColumn();
        public abstract object CreateCollection(ICrmContext context, PropertyInfo p, Guid Parentid, ICollection listc);
    }
    public class TrackedForeingKeyAttribute<H, P> : TrackedForeingKeyAttributeBase where H : ITrackedBusinessObject, new() where P : ITrackedBusinessObject
    {

        public TrackedForeingKeyAttribute(string parentreferencecolumnname)
        {
            Parentreferencecolumnname = parentreferencecolumnname;
        }

        public string Parentreferencecolumnname { get; }
        public override string GetParentReferenceColumn() { return Parentreferencecolumnname; }

        public TrackableCollection<P, H> CreateCollectionInternal(ICrmContext context, PropertyInfo p, Guid Parentid)
        {
            return new TrackableCollection<P, H>(context, p, Parentreferencecolumnname, Parentid);
        }

        public override object CreateCollection(ICrmContext context, PropertyInfo p, Guid Parentid, ICollection listc)
        {
            var x = CreateCollectionInternal(context, p, Parentid);
            if (listc != null)
                foreach (var w in listc)
                {
                    if (w is H converted)
                    {
                        x.Add(converted);
                    }
                }
            return x;
        }
    }
}
