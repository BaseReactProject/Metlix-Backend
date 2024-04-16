using FluentValidation;

namespace Application.Features.ContentGenres.Commands.Update;

public class UpdateContentGenreCommandValidator : AbstractValidator<UpdateContentGenreCommand>
{
    public UpdateContentGenreCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.GenreId).NotEmpty();
    }
}