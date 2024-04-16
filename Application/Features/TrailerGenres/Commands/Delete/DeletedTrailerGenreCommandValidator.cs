using FluentValidation;

namespace Application.Features.TrailerGenres.Commands.Delete;

public class DeleteTrailerGenreCommandValidator : AbstractValidator<DeleteTrailerGenreCommand>
{
    public DeleteTrailerGenreCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}