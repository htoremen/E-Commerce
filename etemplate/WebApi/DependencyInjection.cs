using Persistence.Persistence;
using FluentValidation;
using WebApi.Infrastructure;
using System.Reflection;

namespace WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddInWebApi(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddHttpContextAccessor();

        services.AddHealthChecks();
        //services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        services.AddExceptionHandler<CustomExceptionHandler>();

        return services;
    }
}
