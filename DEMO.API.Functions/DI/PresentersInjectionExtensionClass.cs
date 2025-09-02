using DEMO.API.D365.Services.Common;
using DEMO.API.D365.Services.Customer.Data.Responses;
using DEMO.API.Functions.Presenters;
using DEMO.API.SQL.Integrations.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DEMO.API.Functions.DI
{
    public static class PresentersInjectionExtensionClass
    {
        public static IServiceCollection AddPresentersInputsDependencies(this IServiceCollection services)
        {
            return services
               .AddScoped<IApiPresenter<BaseServiceResponseMessage>, GenericResponsePresenter>()
               .AddScoped<IApiPresenter<UpsertCustomerResponse>, GenericResponsePresenter>()
               .AddScoped<ILogIntegrationRepository, LogIntegrationRepository>();
        }
    }
}
