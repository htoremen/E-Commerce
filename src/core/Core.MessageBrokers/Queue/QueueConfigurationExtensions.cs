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

        queueConfiguration.Names.Add(QueueName.Saga, "Todo." + QueueName.Saga.ToString());
        queueConfiguration.Names.Add(QueueName.CreateTodo, "Todo." + QueueName.CreateTodo.ToString());
        queueConfiguration.Names.Add(QueueName.AddTodoItem, "Todo." + QueueName.AddTodoItem.ToString()); 
        queueConfiguration.Names.Add(QueueName.DeleteTodo, "Todo." + QueueName.DeleteTodo.ToString());
        queueConfiguration.Names.Add(QueueName.CompleteTodo, "Todo." + QueueName.CompleteTodo.ToString());

        if (services != null)
            services.AddSingleton<IQueueConfiguration>(queueConfiguration);

        return services;
    }
}