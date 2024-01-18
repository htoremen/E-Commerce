namespace Core.Events.Parameters;

public interface IStart
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string CurrentState { get; set; }
    public DateTime CreatedOn { get; set; }

}
public class Start : IStart
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string CurrentState { get; set; }
    public DateTime CreatedOn { get; set; }

}