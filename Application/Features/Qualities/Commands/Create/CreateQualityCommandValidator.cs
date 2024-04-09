using FluentValidation;

namespace Application.Features.Qualities.Commands.Create;

public class CreateQualityCommandValidator : AbstractValidator<CreateQualityCommand>
{
    public CreateQualityCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Value).NotEmpty();
    }
}