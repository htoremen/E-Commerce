using Application;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Saga.Application;
using Saga.Persistence;
using Saga.Service;

var builder = WebApplication.CreateBuilder(args);

var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);

builder.Services
   .AddInApplication()
   .AddInPersistence()
   .AddInService(appSettings);


var app = builder.Build();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});



//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;
//    var _context = serviceProvider.GetService<StateDbContext>();

//    _context.Database.EnsureDeleted();
//    _context.Database.EnsureCreated();
//}

app.Run();
