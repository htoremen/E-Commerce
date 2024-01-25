using Core.Events.Customers;
using Core.MessageBrokers;
using Identity.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Users.EventHandlers;

public class CreateCustomerEventHandler : INotificationHandler<CreateCustomerEvent>
{
    private readonly ILogger<CreateCustomerEventHandler> _logger;
    private readonly IMessageSender<ICreateCustomer> _messageSender;

    public CreateCustomerEventHandler(ILogger<CreateCustomerEventHandler> logger, IMessageSender<ICreateCustomer> messageSender)
    {
        _logger = logger;
        _messageSender = messageSender;
    }

    public async Task Handle(CreateCustomerEvent notification, CancellationToken cancellationToken)
    {
        await _messageSender.SendAsync(new CreateCustomer
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
