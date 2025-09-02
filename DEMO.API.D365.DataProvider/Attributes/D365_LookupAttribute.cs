namespace DEMO.API.D365.DataProvider.Attributes
{
    public abstract class D365_LookupAttributeBase : D365_AtributeBase
    {
        public abstract string GetReferenceLogicalEntityName();
    }

    public class D365_LookupAttribute : D365_LookupAttributeBase
    {
        private string _logicalReferenceEntityName = string.Empty;

        public D365_LookupAttribute(string logicalReferenceEntityName)
        {
            _logicalReferenceEntityName = logicalReferenceEntityName;
        }

        public override string GetReferenceLogicalEntityName() { return _logicalReferenceEntityName; }
    }
}
