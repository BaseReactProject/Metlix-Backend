using FluentValidation;

namespace Application.Features.ContentDirectors.Commands.Update;

public class UpdateContentDirectorCommandValidator : AbstractValidator<UpdateContentDirectorCommand>
{
    public UpdateContentDirectorCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.PersonId).NotEmpty();
    }
}