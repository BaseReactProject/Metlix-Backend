using FluentValidation;

namespace Application.Features.ContentActors.Commands.Delete;

public class DeleteContentActorCommandValidator : AbstractValidator<DeleteContentActorCommand>
{
    public DeleteContentActorCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}