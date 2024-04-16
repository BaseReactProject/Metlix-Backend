using FluentValidation;

namespace Application.Features.ContentScenarists.Commands.Create;

public class CreateContentScenaristCommandValidator : AbstractValidator<CreateContentScenaristCommand>
{
    public CreateContentScenaristCommandValidator()
    {
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.PersonId).NotEmpty();
    }
}