using Core.MessageBrokers;
using Microsoft.Extensions.DependencyInjection;

public static class QueueConfigurationExtensions
{
    public static IServiceCollection AddQueueConfiguration(this IServiceCollection services, out IQueueConfiguration queueConfiguration)
    {
        queueConfiguration = new QueueConfiguration()
        {
            Names = new Dictionary<QueueName, string>()
        };

        queueConfiguration.Names.Add(QueueName.Saga, "Parameter." + QueueName.Saga.ToString());
        queueConfiguration.Names.Add(QueueName.Start, "Parameter." + QueueName.Start.ToString());
        queueConfiguration.Names.Add(QueueName.AddParameter, "Parameter." + QueueName.AddParameter.ToString()); 
        queueConfiguration.Names.Add(QueueName.Completed, "Parameter." + QueueName.Completed.ToString()); 

        if (services != null)
            services.AddSingleton<IQueueConfiguration>(queueConfiguration);

        return services;
    }
}