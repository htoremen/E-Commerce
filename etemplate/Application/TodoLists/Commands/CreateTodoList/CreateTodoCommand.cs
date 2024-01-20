namespace Application.TodoLists.Commands.CreateTodoList;

public record CreateTodoCommand : IRequest<string>
{
    public string? Title { get; init; }
    public string? UserId { get; init; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoCommand, string>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList
        {
            Title = request.Title,
            UserId = request.UserId
        };

        _context.TodoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
