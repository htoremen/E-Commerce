namespace Todo.Domain.Entities
{
    public class TodoItem : BaseEntity
    {
        public string TodoListId { get; set; }

        public string? Title { get; set; }

        public string? Note { get; set; }
        public string UserId { get; set; }

        public PriorityLevel Priority { get; set; }

        public DateTime? Reminder { get; set; }


        public virtual TodoList TodoList { get; set; }

        private bool _done;
        public bool Done
        {
            get => _done;
            set
            {
                if (value && !_done)
                {
                    AddDomainEvent(new TodoItemCompletedEvent(this));
                }

                _done = value;
            }
        }
    }
}