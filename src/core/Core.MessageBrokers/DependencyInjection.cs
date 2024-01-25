using Core.MessageBrokers.MessageBrokers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core.MessageBrokers;

public static class DependencyInjection
{
    public static IServiceCollection AddHealthChecksServices(this IServiceCollection services, MessageBrokerOptions appSettings)
    {
        var rabbitMQ = appSettings.RabbitMQ;
        services.AddHealthChecks()
            .AddRabbitMQ(rabbitConnectionString: MessageBrokersCollectionExtensions.GetRabbitMqConnection(rabbitMQ.Password, rabbitMQ.HostName, rabbitMQ.VirtualHost));
        // .AddCheck<RedisHealtCheck>("order-redis");
        return services;
    }

    public static IServiceCollection ConfigureMassTransitHostOptions(this IServiceCollection services, MessageBrokerOptions messageBroker)
    {
        services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
        services.Configure<MassTransitHostOptions>(options =>
        {
            options.WaitUntilStarted = true;
            options.StartTimeout = TimeSpan.FromMinutes(5);
            options.StopTimeout = TimeSpan.FromMinutes(1);
        });

        var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(messageBroker.RabbitMQ.HostName, messageBroker.RabbitMQ.VirtualHost, h =>
            {
                h.Username(messageBroker.RabbitMQ.UserName);
                h.Password(messageBroker.RabbitMQ.Password);
            });
        });

        services.AddSingleton<IPublishEndpoint>(bus);
        services.AddSingleton<ISendEndpointProvider>(bus);
        services.AddSingleton<IBus>(bus);
        services.AddSingleton<IBusControl>(bus);

        return services;
    }

    public static IServiceCollection AddStaticValues(this IServiceCollection services, MessageBrokerOptions appSettings)
    {
        var rabbitMQ = appSettings.RabbitMQ;
        RabbitMQStaticValues.ResetInterval = rabbitMQ.ResetInterval;
        RabbitMQStaticValues.RetryTimeInterval = rabbitMQ.RetryTimeInterval;
        RabbitMQStaticValues.RetryCount = rabbitMQ.RetryCount;
        RabbitMQStaticValues.PrefetchCount = rabbitMQ.PrefetchCount;
        RabbitMQStaticValues.TrackingPeriod = rabbitMQ.TrackingPeriod;
        RabbitMQStaticValues.ActiveThreshold = rabbitMQ.ActiveThreshold;
        RabbitMQStaticValues.Password = rabbitMQ.Password;
        RabbitMQStaticValues.HostName = rabbitMQ.HostName;
        RabbitMQStaticValues.VirtualHost = rabbitMQ.VirtualHost;
        RabbitMQStaticValues.ConnectionString = rabbitMQ.ConnectionString;

        return services;
    }
}
