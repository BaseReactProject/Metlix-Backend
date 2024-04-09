using FluentValidation;

namespace Application.Features.Plans.Commands.Delete;

public class DeletePlanCommandValidator : AbstractValidator<DeletePlanCommand>
{
    public DeletePlanCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}