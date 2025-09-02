namespace DEMO.API.Functions.Helpers.Types.Config
{

    using Microsoft.Extensions.Configuration;
    public class AppSettingConfiguration : IAppConfiguration
    {
        private readonly IConfiguration? configurationSource;
        private Lazy<AppConfiguration> appConfiguration;
        public AppSettingConfiguration(IConfiguration? configurationSource = null)
        {
            this.configurationSource = configurationSource;
            this.appConfiguration = new Lazy<AppConfiguration>(() =>
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

                return new AppConfiguration
                {
                    SQLConnectionString = config["SQLConnectionString"],
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

        public AppConfiguration GetConfiguration()
        {
            return appConfiguration.Value;
        }
    }
}
