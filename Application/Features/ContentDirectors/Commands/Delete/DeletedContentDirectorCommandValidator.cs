using FluentValidation;

namespace Application.Features.ContentDirectors.Commands.Delete;

public class DeleteContentDirectorCommandValidator : AbstractValidator<DeleteContentDirectorCommand>
{
    public DeleteContentDirectorCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}