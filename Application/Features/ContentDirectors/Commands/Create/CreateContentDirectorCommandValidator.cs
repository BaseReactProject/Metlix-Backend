using FluentValidation;

namespace Application.Features.ContentDirectors.Commands.Create;

public class CreateContentDirectorCommandValidator : AbstractValidator<CreateContentDirectorCommand>
{
    public CreateContentDirectorCommandValidator()
    {
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.PersonId).NotEmpty();
    }
}