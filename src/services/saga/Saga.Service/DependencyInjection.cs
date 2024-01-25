using Core.Events.TodoItems;
using Core.Events.TodoLists;
using Core.MessageBrokers;
using Core.MessageBrokers.Enums;
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
        services.AddStaticValues(appSettings.MessageBroker);

        services.AddQueueConfiguration(out IQueueConfiguration queueConfiguration);
        var messageBroker = appSettings.MessageBroker;
        var rabbitMQ = messageBroker.RabbitMQ;

        services.AddMassTransit(x => { UsingRabbitMq(x, appSettings, queueConfiguration); });
        services.ConfigureMassTransitHostOptions(messageBroker);
        services.AddHealthChecks()
            .AddRabbitMQ(MessageBrokersCollectionExtensions.GetRabbitMqConnection(rabbitMQ.Password, rabbitMQ.HostName, rabbitMQ.VirtualHost));
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

                    s.Message<ICreateTodo>(x => x.UsePartitioner(partition, m => m.Message.SessionId));
                    s.Message<IAddTodoItem>(x => x.UsePartitioner(partition, m => m.Message.SessionId));
                    s.Message<IDeleteTodo>(x => x.UsePartitioner(partition, m => m.Message.SessionId));
                    s.Message<ICompleteTodo>(x => x.UsePartitioner(partition, m => m.Message.SessionId));
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


}