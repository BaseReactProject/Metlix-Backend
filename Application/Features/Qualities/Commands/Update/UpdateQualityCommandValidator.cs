using FluentValidation;

namespace Application.Features.Qualities.Commands.Update;

public class UpdateQualityCommandValidator : AbstractValidator<UpdateQualityCommand>
{
    public UpdateQualityCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Value).NotEmpty();
    }
}