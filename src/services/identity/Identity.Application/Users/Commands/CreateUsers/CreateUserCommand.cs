namespace Identity.Application.Users;

public class CreateUserCommand : IRequest<GenericResponse<CreateUserResponse>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public LoginType LoginType { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GenericResponse<CreateUserResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public CreateUserCommandHandler(IApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<GenericResponse<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
        if (user == null)
        {
            var random = new Random();
            var newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                LoginType = request.LoginType,
            };
            _context.Users.Add(newUser);

            var response = await _context.SaveChangesAsync(cancellationToken);
            if (response > 0)
                await _mediator.Publish(new CreateCustomerEvent(newUser));
            return GenericResponse<CreateUserResponse>.Success(new CreateUserResponse { }, 200);
        }
        else
        {
            return GenericResponse<CreateUserResponse>.Success("Mail adresi sistemde kayıtlıdır.", 200);
        }
    }
}
