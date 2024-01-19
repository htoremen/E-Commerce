using Domain.ValueObjects;

namespace Domain.Entities;

public class TodoList :BaseEntity
{
    public string Title { get; set; }
    public string UserId {  get; set; }
    public Colour Colour { get; set; } = Colour.White;
    public virtual ICollection<TodoItem> Items { get; set; }
}
