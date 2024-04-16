using FluentValidation;

namespace Application.Features.ContentGenres.Commands.Create;

public class CreateContentGenreCommandValidator : AbstractValidator<CreateContentGenreCommand>
{
    public CreateContentGenreCommandValidator()
    {
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.GenreId).NotEmpty();
    }
}