using FluentValidation;

namespace Application.Parameters.Command.AddParameters;

public class AddParameterValidator : AbstractValidator<AddParameterCommand>
{
    public AddParameterValidator()
    {
        RuleFor(request => request.Data.Name).NotEmpty().MaximumLength(50);
        RuleFor(request => request.Data.IsActive).NotEmpty();
    }
}