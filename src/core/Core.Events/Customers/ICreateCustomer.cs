namespace Core.Events.Customers;

public interface ICreateCustomer
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }

    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
public class CreateCustomer : ICreateCustomer
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }

    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
