namespace DEMO.API.D365.DataProvider.DI
{
    using DEMO.API.D365.DataProvider.Config;
    using DEMO.API.D365.DataProvider.CrmConnectionProvider;
    using DEMO.API.D365.DataProvider.CrmContext;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionExtensionClass
    {
        public static IServiceCollection AddCDSDataProviderDependencies(this IServiceCollection services)
        {
            return services.AddScoped<ICrmContext, CrmContext>()
               .AddSingleton<ICrmConnectionsProvider, CrmConnectionsProvider>()
               .AddSingleton<ICDSConfigurationProvider, AppSettingConfigurationProvider>();
        }
    }
}
