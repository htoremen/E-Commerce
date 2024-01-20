using Microsoft.Extensions.DependencyInjection;

namespace Saga.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInPersistence(this IServiceCollection services)
    {
        return services;
    }

}