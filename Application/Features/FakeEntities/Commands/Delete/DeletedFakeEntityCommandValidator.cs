using FluentValidation;

namespace Application.Features.FakeEntities.Commands.Delete;

public class DeleteFakeEntityCommandValidator : AbstractValidator<DeleteFakeEntityCommand>
{
    public DeleteFakeEntityCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}