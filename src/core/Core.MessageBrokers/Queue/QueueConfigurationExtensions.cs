using Core.MessageBrokers;
using Core.MessageBrokers.Enums;
using Microsoft.Extensions.DependencyInjection;

public static class QueueConfigurationExtensions
{
    public static IServiceCollection AddQueueConfiguration(this IServiceCollection services, out IQueueConfiguration queueConfiguration)
    {
        queueConfiguration = new QueueConfiguration()
        {
            Names = new Dictionary<QueueName, string>()
        };

        queueConfiguration.Names.Add(QueueName.Start, "Meet." + QueueName.Start.ToString()); // Saga

    

        if (services != null)
            services.AddSingleton<IQueueConfiguration>(queueConfiguration);

        return services;
    }
}