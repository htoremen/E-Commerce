﻿using Core.MessageBrokers.MessageBrokers;

namespace Core.MessageBrokers.RabbitMQ;

public class RabbitMQSender<T> : IMessageSender<T>
{
    private readonly ISendEndpoint _sendEndpoint;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IQueueConfiguration _queueConfiguration;

    public RabbitMQSender(ISendEndpointProvider sendEndpointProvider, IQueueConfiguration queueConfiguration, IPublishEndpoint publishEndpoint)
    {
        _queueConfiguration = queueConfiguration;
        var sendToUri = MessageBrokersCollectionExtensions.GetRabbitMqConnection();
        _sendEndpoint = sendEndpointProvider.GetSendEndpoint(new($"queue:{_queueConfiguration.Names[QueueName.Saga]}")).Result;
       // _sendEndpoint = sendEndpointProvider.GetSendEndpoint(new Uri(RabbitMQStaticValues.ConnectionString)).Result;
        _publishEndpoint = publishEndpoint;
    }

    public async Task SendAsync(T message, MetaData metaData = null, CancellationToken cancellationToken = default)
    {
        await _sendEndpoint.Send(message, cancellationToken);
    }

    public async Task Publish(T message, MetaData metaData = null, CancellationToken cancellationToken = default)
    {
        await _publishEndpoint.Publish(message, cancellationToken);
    }
}