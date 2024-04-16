using FluentValidation;

namespace Application.Features.TrailerGenres.Commands.Create;

public class CreateTrailerGenreCommandValidator : AbstractValidator<CreateTrailerGenreCommand>
{
    public CreateTrailerGenreCommandValidator()
    {
        RuleFor(c => c.TrailerId).NotEmpty();
        RuleFor(c => c.GenreId).NotEmpty();
    }
}