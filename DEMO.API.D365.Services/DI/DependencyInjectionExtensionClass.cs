namespace DEMO.API.D365.Services.DI
{
    using DEMO.API.D365.Services.Customer;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionExtensionClass
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            return services
               .AddScoped<ICustomerService, CustomerService>();
        }
    }
}
