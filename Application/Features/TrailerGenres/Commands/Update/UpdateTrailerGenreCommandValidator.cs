using FluentValidation;

namespace Application.Features.TrailerGenres.Commands.Update;

public class UpdateTrailerGenreCommandValidator : AbstractValidator<UpdateTrailerGenreCommand>
{
    public UpdateTrailerGenreCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.TrailerId).NotEmpty();
        RuleFor(c => c.GenreId).NotEmpty();
    }
}