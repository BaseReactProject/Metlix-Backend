using FluentValidation;

namespace Application.Features.ContentGenres.Commands.Delete;

public class DeleteContentGenreCommandValidator : AbstractValidator<DeleteContentGenreCommand>
{
    public DeleteContentGenreCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}