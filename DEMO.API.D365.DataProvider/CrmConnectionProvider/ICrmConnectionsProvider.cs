namespace DEMO.API.D365.DataProvider.CrmConnectionProvider
{
    using Microsoft.PowerPlatform.Dataverse.Client;

    public interface ICrmConnectionsProvider
    {
        ServiceClient GetDataverseConnection();
    }
}
