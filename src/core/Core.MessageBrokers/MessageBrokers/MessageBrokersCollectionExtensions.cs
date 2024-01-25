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

    public static Uri GetRabbitMqConnection(string password, string hostName, string virtualHost)
    {
        var connectionString = $"amqp://{password}:{password}@{hostName}{virtualHost}";
        Uri uri = new Uri(connectionString, UriKind.Absolute);

        return uri;
    }
}
