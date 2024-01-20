using Core.Events.TodoLists;

namespace Saga.Application.TodoLists;

public class DeleteTodoCommand : IDeleteTodo
{
    public DeleteTodoCommand(Guid correlationId)
    {
        CorrelationId = correlationId;
    }

    public Guid CorrelationId { get; set; }
    public Guid SessionId { get; set; }
    public string UserId { get; set; }
    public string CurrentState { get; set; }
    public DateTime CreatedOn { get; set; }
}