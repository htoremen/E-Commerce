namespace Domain.Events;

public class TodoItemDeletedEvent : BaseEvent
{
    public TodoItem TodoItem { get; }

    public TodoItemDeletedEvent(TodoItem todoItem)
    {
        TodoItem = todoItem;
    }
}
