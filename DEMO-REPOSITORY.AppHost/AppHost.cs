var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureFunctionsProject<Projects.DEMO_API_Functions>("demo-api-functions");

builder.Build().Run();
