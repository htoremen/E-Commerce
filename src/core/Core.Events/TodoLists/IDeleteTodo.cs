namespace Core.Events.TodoLists;

public interface IDeleteTodo
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string UserId { get; set; }
    public string CurrentState { get; set; }
    public DateTime CreatedOn { get; set; }
}

public class DeleteTodo : IDeleteTodo
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string UserId { get; set; }
    public string CurrentState { get; set; }
    public DateTime CreatedOn { get; set; }
}
