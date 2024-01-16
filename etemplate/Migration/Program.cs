using Application;
using Infrastructure;
using Persistence;
using Migration;

IConfiguration Configuration;
var builder = WebApplication.CreateBuilder(args);

IWebHostEnvironment environment = builder.Environment;
Configuration = builder.Configuration
        .AddJsonFile($"appsettings.json", false, true)
        .AddEnvironmentVariables()
        .AddCommandLine(args)
        .AddUserSecrets<Program>()
        .Build();

builder.Services
   .AddInApplication()
   .AddInfrastructure()
   .AddInPersistence(builder.Configuration);


builder.Services.AddScoped<IMigrationService, MigrationService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var migrationService = serviceProvider.GetRequiredService<IMigrationService>();
    if (migrationService != null)
    {
        migrationService.Migrate();
    }
}