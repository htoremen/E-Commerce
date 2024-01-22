
namespace Todo.Application.TodoItems
{
    public class TodoItemBriefDto
    {
        public string Id { get; init; }

        public string TodoListId { get; init; }

        public string? Title { get; init; }

        public bool Done { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<TodoItem, TodoItemBriefDto>();
            }
        }
    }
}