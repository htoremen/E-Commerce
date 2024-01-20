namespace Core.Events.TodoLists;

public interface ICreateTodo
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string CurrentState { get; set; }
    public DateTime CreatedOn { get; set; }
}
public class CreateTodo : ICreateTodo
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string CurrentState { get; set; }
    public DateTime CreatedOn { get; set; }
}
