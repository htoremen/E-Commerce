using Application.Abstractions;
using Domain;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Parameters.Command.AddParameters;

public class AddParameterCommand : IRequest<GenericResponse<AddParameterResponse>>
{
    public AddParameterRequest Data { get; set; }
}

public class AddParameterCommandHandler : IRequestHandler<AddParameterCommand, GenericResponse<AddParameterResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddParameterCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GenericResponse<AddParameterResponse>> Handle(AddParameterCommand request, CancellationToken cancellationToken)
    {
        var validationResult = new AddParameterValidator().Validate(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var parameter = new Parameter { Name = request.Data.Name, ParameterTypeId = request.Data.ParameterTypeId, IsActive = request.Data.IsActive, CreatedDate=DateTime.Now };
        _unitOfWork.Parameter.Add(parameter);
        _unitOfWork.Commit();

        return GenericResponse<AddParameterResponse>.Success(new AddParameterResponse { Id = parameter.Id }, 200);
    }
}
