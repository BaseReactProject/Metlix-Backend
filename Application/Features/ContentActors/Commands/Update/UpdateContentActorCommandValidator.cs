using FluentValidation;

namespace Application.Features.ContentActors.Commands.Update;

public class UpdateContentActorCommandValidator : AbstractValidator<UpdateContentActorCommand>
{
    public UpdateContentActorCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.PersonId).NotEmpty();
    }
}