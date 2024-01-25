namespace Identity.Application.Users;
public class CreateCustomerEvent : BaseEvent
{
    public CreateCustomerEvent(User user)
    {
        User = user;
    }
    public User User { get; }
}


public class CreateCustomerEventHandler : INotificationHandler<CreateCustomerEvent>
{
    private readonly ILogger<CreateCustomerEventHandler> _logger;
    private readonly IMessageSender<ICreateCustomer> _messageSender;
    private readonly ISendEndpoint _sendEndpoint;

    public CreateCustomerEventHandler(ILogger<CreateCustomerEventHandler> logger, IMessageSender<ICreateCustomer> messageSender, ISendEndpointProvider sendEndpointProvider)
    {
        _logger = logger;
        _sendEndpoint = sendEndpointProvider.GetSendEndpoint(new($"queue:Customer.CreateCustomer")).Result;
    }

    public async Task Handle(CreateCustomerEvent notification, CancellationToken cancellationToken)
    {
        await _sendEndpoint.Send(new CreateCustomer
        {
            CorrelationId = Guid.NewGuid(),
            SessionId = Guid.NewGuid(),
            Id = notification.User.Id,
            FirstName = notification.User.FirstName,
            LastName = notification.User.LastName,
        });
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
    }
}
