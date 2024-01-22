using Microsoft.EntityFrameworkCore;

namespace Todo.Application.Common.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<Parameter> Parameters { get; }
        DbSet<ParameterType> ParameterTypes { get; }

        DbSet<TodoList> TodoLists { get; }
        DbSet<TodoItem> TodoItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}