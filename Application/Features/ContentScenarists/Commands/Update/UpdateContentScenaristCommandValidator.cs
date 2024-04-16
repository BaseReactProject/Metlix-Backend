using FluentValidation;

namespace Application.Features.ContentScenarists.Commands.Update;

public class UpdateContentScenaristCommandValidator : AbstractValidator<UpdateContentScenaristCommand>
{
    public UpdateContentScenaristCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.PersonId).NotEmpty();
    }
}