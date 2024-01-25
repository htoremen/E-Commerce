using Core.Events.Customers;

namespace Customer.Application.Customers;

public class CreateCustomerCommand : CreateCustomer, IRequest<GenericResponse<CreateCustomerResponse>>
{
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, GenericResponse<CreateCustomerResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GenericResponse<CreateCustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<Domain.Entities.Customer>(request);
        _context.Customers.Add(command);
        await _context.SaveChangesAsync(cancellationToken);
        return GenericResponse<CreateCustomerResponse>.Success(new CreateCustomerResponse(), 200);
    }
}
