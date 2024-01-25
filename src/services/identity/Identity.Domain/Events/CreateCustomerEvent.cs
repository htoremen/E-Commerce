namespace Identity.Domain.Events;

public class CreateCustomerEvent : BaseEvent
{
    public CreateCustomerEvent(User user)
    {
        User = user;
    }
    public User User { get; set; }
}
