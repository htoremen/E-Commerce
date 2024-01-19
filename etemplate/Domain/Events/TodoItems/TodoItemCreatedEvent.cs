namespace Domain.Events;

public class TodoItemCreatedEvent : BaseEvent
{
    public TodoItemCreatedEvent(TodoItem todoItem)
    {
        TodoItem = todoItem;
    }

    public TodoItem TodoItem { get; }
}
