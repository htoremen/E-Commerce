using Core.Events.Parameters;
using Core.MessageBrokers;
using Core.MessageBrokers.Enums;
using Core.MessageBrokers.MessageBrokers;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Saga.Application;
using Saga.Persistence;
using System.Reflection;

namespace Saga.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddInService(this IServiceCollection services, AppSettings appSettings)
    {
        AddStaticValues(services, appSettings);

        services.AddQueueConfiguration(out IQueueConfiguration queueConfiguration);
        var messageBroker = appSettings.MessageBroker;

        services.AddMassTransit(x => { UsingRabbitMq(x, appSettings, queueConfiguration); });
        services.ConfigureMassTransitHostOptions(messageBroker);

        services.AddHealthChecks()
            .AddRabbitMQ(GetRabbitMqConnection(appSettings));
        return services;
    }
    private static IServiceCollection AddStaticValues(this IServiceCollection services, AppSettings appSettings)
    {
        var rabbitMQ = appSettings.MessageBroker.RabbitMQ;
        RabbitMQStaticValues.ResetInterval = rabbitMQ.ResetInterval;
        RabbitMQStaticValues.RetryTimeInterval = rabbitMQ.RetryTimeInterval;
        RabbitMQStaticValues.RetryCount = rabbitMQ.RetryCount;
        RabbitMQStaticValues.PrefetchCount = rabbitMQ.PrefetchCount;
        RabbitMQStaticValues.TrackingPeriod = rabbitMQ.TrackingPeriod;
        RabbitMQStaticValues.ActiveThreshold = rabbitMQ.ActiveThreshold;

        return services;
    }

    [Obsolete]
    private static void UsingRabbitMq(IBusRegistrationConfigurator x, AppSettings appSettings, IQueueConfiguration queueConfiguration)
    {
        var config = appSettings.MessageBroker.RabbitMQ;

        x.SetKebabCaseEndpointNameFormatter();
        x.AddSagas(Assembly.GetExecutingAssembly());
        x.AddSagasFromNamespaceContaining<StateInstance>();
        x.AddSagasFromNamespaceContaining(typeof(StateInstance));

        x.AddSagaStateMachine<StateMachine, StateInstance, SagaStateDefinition>()
            .EntityFrameworkRepository(config =>
            {
                config.ConcurrencyMode = ConcurrencyMode.Pessimistic; // or use Optimistic, which requires RowVersion

                config.AddDbContext<DbContext, StateDbContext>((p, b) =>
                {
                    b.UseSqlServer(appSettings.ConnectionStrings.ConnectionString, m =>
                    {
                    });

                });
            });

        x.AddBus(factory => Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.UseJsonSerializer();
            cfg.UseRetry(c => c.Interval(config.RetryCount, config.ResetInterval));

            cfg.Host(config.HostName, config.VirtualHost, h =>
            {
                h.Username(config.UserName);
                h.Password(config.Password);
            });
            cfg.ReceiveEndpoint(queueConfiguration.Names[QueueName.Saga], e =>
            {
                const int ConcurrencyLimit = 20; // this can go up, depending upon the database capacity

                e.ConfigureSaga<StateInstance>(factory, s =>
                {
                    var partition = e.CreatePartitioner(ConcurrencyLimit);

                    s.Message<IStart>(x => x.UsePartitioner(partition, m => m.Message.SessionId));
                    s.Message<IAddParameter>(x => x.UsePartitioner(partition, m => m.Message.SessionId));
                    s.Message<ICompleted>(x => x.UsePartitioner(partition, m => m.Message.SessionId));
                });
            });
        }));
    }

    private static IServiceCollection ConfigureMassTransitHostOptions(this IServiceCollection services, MessageBrokerOptions messageBroker)
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

    private static Uri GetRabbitMqConnection(AppSettings appSettings)
    {
        var config = appSettings.MessageBroker.RabbitMQ;
        //ConnectionFactory factory = new ConnectionFactory
        //{
        //    UserName = appSettings.MessageBroker.RabbitMQ.UserName,
        //    Password = appSettings.MessageBroker.RabbitMQ.Password,
        //    VirtualHost = appSettings.MessageBroker.RabbitMQ.VirtualHost,
        //    HostName = appSettings.MessageBroker.RabbitMQ.HostName,
        //    Port = AmqpTcpEndpoint.UseDefaultPort
        //};

        //var connection = factory.CreateConnection();

        var connectionString = $"amqp://{config.Password}:{config.Password}@{config.HostName}{config.VirtualHost}";
        Uri uri = new Uri(connectionString, UriKind.Absolute);

        return uri;
    }

}