using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var appSettings = new AppSettings();
builder.Configuration.Bind(appSettings);

builder.Services
   .AddInApplication(appSettings)
   .AddInfrastructure()
   .AddInPersistence(builder.Configuration)
   .AddInWebApi(appSettings)
   .AddEventBus(appSettings)
   .AddHealthChecksServices(appSettings.MessageBroker)
   .AddStaticValues(appSettings.MessageBroker);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;
//    var _context = serviceProvider.GetService<ApplicationDbContext>();
//    ApplicationDbContextSeed.Migrate(_context);
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.MapControllers();

app.Run();
