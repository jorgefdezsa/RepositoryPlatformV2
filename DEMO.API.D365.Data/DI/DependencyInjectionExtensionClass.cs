namespace DEMO.API.D365.Data.DI
{
    using DEMO.API.Core.Domain.Entities;
    using DEMO.API.D365.Data.Data.Repositories;
    using DEMO.API.D365.Data.Data.Repositories.Customer;
    using DEMO.API.D365.Data.Data.Repositories.Masters;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionExtensionClass
    {
        public static IServiceCollection AddRepositoriesDependencies(this IServiceCollection services)
        {
            return services
               .AddScoped<ICustomerRepository, CustomerRepository>()
               .AddScoped<IMasterEntityRepository<LegalEntity>, MasterEntityRepository<LegalEntity>>()
               .AddScoped<IRepository<LegalEntity>, Repository<LegalEntity>>();
        }
    }
}
