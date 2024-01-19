namespace Domain.Events;

public class TodoItemCompletedEvent : BaseEvent
{
    public TodoItem TodoItem { get; }

    public TodoItemCompletedEvent(TodoItem todoItem)
    {
        TodoItem = todoItem;
    }
}
