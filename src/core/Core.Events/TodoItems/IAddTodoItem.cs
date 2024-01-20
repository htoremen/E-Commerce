namespace Core.Events.TodoItems;

public interface IAddTodoItem
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string UserId { get; set; }
    public int TodoListId { get; set; }
    public string? Title { get; set; }
    public string? Note { get; set; }
    public string CurrentState { get; set; }
    public DateTime CreatedOn { get; set; }
}

public class AddTodoItem : IAddTodoItem
{
    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string UserId { get; set; }
    public int TodoListId { get; set; }
    public string? Title { get; set; }
    public string? Note { get; set; }
    public string CurrentState { get; set; }
    public DateTime CreatedOn { get; set; }
}
