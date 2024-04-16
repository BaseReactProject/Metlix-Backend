using FluentValidation;

namespace Application.Features.ContentActors.Commands.Create;

public class CreateContentActorCommandValidator : AbstractValidator<CreateContentActorCommand>
{
    public CreateContentActorCommandValidator()
    {
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.PersonId).NotEmpty();
    }
}