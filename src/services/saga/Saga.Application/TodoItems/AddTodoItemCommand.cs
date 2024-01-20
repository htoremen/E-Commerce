using Core.Events.TodoItems;

namespace Saga.Application.Parameters;

public class AddTodoItemCommand : IAddTodoItem
{
    public AddTodoItemCommand(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string UserId { get; set; }
    public int TodoListId { get; set; }
    public string? Title { get; set; }
    public string? Note { get; set; }
    public string CurrentState { get; set; }
    public DateTime CreatedOn { get; set; }
}
