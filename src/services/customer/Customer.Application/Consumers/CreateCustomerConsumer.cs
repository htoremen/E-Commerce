using Core.Events.Customers;
using Core.MessageBrokers.RabbitMQ;
using Customer.Application.Customers;
using MassTransit;

namespace Customer.Application.Consumers;

public class CreateCustomerConsumer : IConsumer<ICreateCustomer>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateCustomerConsumer(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<ICreateCustomer> context)
    {
        var command = _mapper.Map<CreateCustomerCommand>(context.Message);
        await _mediator.Send(command);
    }
}



public class CreateCustomerConsumerDefinition : ConsumerDefinition<CreateCustomerConsumer>
{
    public CreateCustomerConsumerDefinition()
    {
        ConcurrentMessageLimit = SetConfigureConsumer.ConcurrentMessageLimit();
    }

    [Obsolete]
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<CreateCustomerConsumer> consumerConfigurator)
    {
        endpointConfigurator.SetConfigure();
    }
}