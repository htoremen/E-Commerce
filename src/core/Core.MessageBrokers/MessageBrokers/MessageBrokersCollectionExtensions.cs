using Core.MessageBrokers.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

namespace Core.MessageBrokers;

public static class MessageBrokersCollectionExtensions
{
    public static IServiceCollection AddRabbitMQSender<T>(this IServiceCollection services)
    {
        services.AddSingleton<IMessageSender<T>, RabbitMQSender<T>>();
        return services;
    }


    public static IServiceCollection AddMessageBusSender<T>(this IServiceCollection services, MessageBrokerOptions options, IHealthChecksBuilder healthChecksBuilder = null, HashSet<string> checkDulicated = null)
    {
        services.AddRabbitMQSender<T>();
        return services;
    }
}
