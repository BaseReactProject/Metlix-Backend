using FluentValidation;

namespace Application.Features.Plans.Commands.Update;

public class UpdatePlanCommandValidator : AbstractValidator<UpdatePlanCommand>
{
    public UpdatePlanCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.QualityId).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.DeviceCount).NotEmpty();
        RuleFor(c => c.Price).NotEmpty();
    }
}