namespace DEMO.API.Functions.Helpers.Types.DI
{
    using DEMO.API.Functions.Helpers.Types.Config;
    using DEMO.API.Functions.Helpers.Types.Config.Retry;
    using DEMO.API.Functions.Helpers.Types.Parsers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionExtensionClass
    {

        public static IServiceCollection AddHelpersDependencies(this IServiceCollection services)
        {
            services
               .AddSingleton<IAppConfiguration, AppSettingConfiguration>()
               .AddScoped<IParserExecutionContextPlugin, XMLParserExecutionContextPlugin>()
               .AddOptions<RetryManagerOptions>()
               .Configure<IConfiguration>((settings, configuration) =>
               {
                   configuration.GetSection("RetryManagerOptions").Bind(settings);
               });
            services.AddOptions<ProcessOptions>()
               .Configure<IConfiguration>((settings, configuration) =>
               {
                   configuration.GetSection("ProcessOptions").Bind(settings);
               });
            return services;
        }
    }
}
