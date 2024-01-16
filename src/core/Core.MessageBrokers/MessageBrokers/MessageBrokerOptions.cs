using Core.Infrastructure.MessageBrokers.RabbitMQ;
using Core.MessageBrokers.RabbitMQ;

namespace Core.MessageBrokers;

public class MessageBrokerOptions
{
    public string Provider { get; set; }
    public RabbitMQOptions RabbitMQ { get; set; }
}

