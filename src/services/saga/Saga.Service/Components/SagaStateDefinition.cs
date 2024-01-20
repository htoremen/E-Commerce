using Core.MessageBrokers.RabbitMQ;
using MassTransit;
using Saga.Persistence;

namespace Saga.Service;

public class SagaStateDefinition : SagaDefinition<StateInstance>
{
    public SagaStateDefinition()
    {
        ConcurrentMessageLimit = SetConfigureConsumer.ConcurrentMessageLimit();
    }

    protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<StateInstance> sagaConfigurator)
    {
        endpointConfigurator.SetConfigure();
    }
}