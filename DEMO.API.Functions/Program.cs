using DEMO.API.D365.Data.DI;
using DEMO.API.D365.DataProvider.DI;
using DEMO.API.D365.Services.DI;
using DEMO.API.Functions.Common;
using DEMO.API.Functions.Data.Mapping;
using DEMO.API.Functions.DI;
using DEMO.API.Functions.Helpers.Types.DI;
using DEMO.API.Functions.Helpers.Types.ServiceBus;
using DEMO.API.Functions.Middlewares;
using DEMO.API.SQL.DI;
using DEMO.API.SQL.Integrations.Models;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


var host = new HostBuilder()
     .ConfigureServices((hostContext, services) =>
     {
         services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
         services.AddAutoMapper(typeof(ExampleFunctionProfile).Assembly);
         services.AddAutoMapper(typeof(MastersProfiles).Assembly);
         services.AddOptions();
         services.AddHttpClient();

         services.AddLogging(configure => configure.AddConsole((c) =>
         {
             c.IncludeScopes = true;
             c.LogToStandardErrorThreshold = LogLevel.Trace;
         }))
         .AddCDSDataProviderDependencies()
         .AddRepositoriesDependencies()
         .AddServicesDependencies()
         .AddHelpersDependencies()
         .AddSqlDependencies()
         .AddPresentersInputsDependencies()
         .AddSingleton<CommonFunctions>()
         .AddAzureClients(builder =>
         {
             builder.AddServiceBusClient(hostContext.Configuration.GetValue<string>("ServiceBusConnection"))
             .WithName("sbDefault")
             .ConfigureOptions(options =>
             {
                 options.RetryOptions.MaxRetries = 20;
             });
             builder.AddServiceBusAdministrationClient(hostContext.Configuration.GetValue<string>("ServiceBusConnection"))
             .WithName("sbAdminDefault");

         });
         services.AddSingleton<IRetryManager, RetryManager>();
         services.AddDbContext<SqlDbDemoContext>(options => options.UseSqlServer(hostContext.Configuration.GetValue<string>("SQLConnectionString")));
     })
    .ConfigureFunctionsWebApplication(workerApplication =>
    {
        workerApplication.UseMiddleware<ExceptionLoggingMiddleware>();
        workerApplication.UseNewtonsoftJson();
    })
    .ConfigureOpenApi()
    .Build();
host.Run();