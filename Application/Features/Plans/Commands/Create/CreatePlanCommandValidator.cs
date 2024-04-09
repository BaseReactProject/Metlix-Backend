using FluentValidation;

namespace Application.Features.Plans.Commands.Create;

public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
{
    public CreatePlanCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.QualityId).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.DeviceCount).NotEmpty();
        RuleFor(c => c.Price).NotEmpty();
    }
}