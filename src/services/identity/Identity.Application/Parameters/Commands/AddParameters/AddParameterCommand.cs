using ValidationException = Identity.Application.Common.Exceptions.ValidationException;

namespace Identity.Application.Parameters.Command.AddParameters
{
    public class AddParameterCommand : IRequest<GenericResponse<AddParameterResponse>>
    {
        public AddParameterRequest Data { get; set; }
    }

    public class AddParameterCommandHandler : IRequestHandler<AddParameterCommand, GenericResponse<AddParameterResponse>>
    {
        private readonly IApplicationDbContext _context;

        public AddParameterCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GenericResponse<AddParameterResponse>> Handle(AddParameterCommand request, CancellationToken cancellationToken)
        {
            var validationResult = new AddParameterValidator().Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var parameter = new Parameter { ParameterName = request.Data.ParameterName, ParameterTypeId = request.Data.ParameterTypeId, IsActive = request.Data.IsActive, CreatedDate = DateTime.Now };
            _context.Parameters.Add(parameter);
            await _context.SaveChangesAsync(cancellationToken);

            return GenericResponse<AddParameterResponse>.Success(new AddParameterResponse { Id = parameter.Id }, 200);
        }
    }
}
