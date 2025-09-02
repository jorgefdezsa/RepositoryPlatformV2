namespace DEMO.API.SQL.DI
{
    using DEMO.API.D365.Data.Cache;
    using DEMO.API.SQL.Repositories;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionExtensionClass
    {
        public static IServiceCollection AddSqlDependencies(this IServiceCollection services)
        {
            return services
                .AddScoped<ILogRequestRepository, LogRequestRepository>()
                .AddSingleton<IDataCache, DataCache>();
        }
    }
}
