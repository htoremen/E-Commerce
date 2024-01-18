namespace Core.Events.Parameters;

public interface ICompleted
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string CurrentState { get; set; }
}

public class Completed : ICompleted
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string CurrentState { get; set; }
}
