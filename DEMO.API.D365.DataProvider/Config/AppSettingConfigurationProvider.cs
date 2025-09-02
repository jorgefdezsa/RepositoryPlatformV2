namespace DEMO.API.D365.DataProvider.Config
{
    using Microsoft.Extensions.Configuration;

    public class AppSettingConfigurationProvider : ICDSConfigurationProvider
    {
        private readonly IConfiguration? configurationSource;
        private Lazy<Configuration> Configuration;
        public AppSettingConfigurationProvider(IConfiguration? configurationSource = null)
        {
            this.configurationSource = configurationSource;
            this.Configuration = new Lazy<Configuration>(() =>
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile("appsettings.json", true);
                builder.AddJsonFile("appsettings.local.json", true);
                builder.AddJsonFile("local.settings.json", true);
                if (configurationSource != null)
                    builder.AddConfiguration(configurationSource);
                IConfigurationRoot config = builder.Build();
                var settingsConfig = config;

                config = builder.Build();

                return new Configuration
                {
                    CRMConnectionString = config["CRMConnectionString"]
                };


            });
        }


        public string Default(string value, string defaultValue)
        {
            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;
            else
                return value;

        }
        public Configuration GetConfiguration()
        {
            return Configuration.Value;
        }
    }
}
