namespace DEMO.API.D365.DataProvider.CrmConnectionProvider
{
    using DEMO.API.D365.DataProvider.Config;
    using Microsoft.Extensions.Logging;
    using Microsoft.PowerPlatform.Dataverse.Client;

    public class CrmConnectionsProvider : IDisposable, ICrmConnectionsProvider
    {

        public CrmConnectionsProvider(ICDSConfigurationProvider configurationProvider, ILogger<CrmConnectionsProvider> logger)
        {
            ConfigurationProvider = configurationProvider;
            Logger = logger;
            dataverseConnection = new Lazy<ServiceClient>(() =>
            {

                Configuration configuration = ConfigurationProvider.GetConfiguration();
                ServiceClient svc = new ServiceClient(configuration.CRMConnectionString);
                return svc;
            });
        }

        private ICDSConfigurationProvider ConfigurationProvider { get; }
        public ILogger<CrmConnectionsProvider> Logger { get; }


        private Lazy<ServiceClient> dataverseConnection;
        private string authUrl = "";
        private static DateTime authCache = DateTime.MinValue;

        public ServiceClient GetDataverseConnection()
        {

            return dataverseConnection.Value;
        }


        public void Dispose()
        {

            if (dataverseConnection.IsValueCreated)
            {
                dataverseConnection.Value.Dispose();
            }
        }
    }
}
