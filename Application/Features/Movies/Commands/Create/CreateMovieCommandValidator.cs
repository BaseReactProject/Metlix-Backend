using FluentValidation;

namespace Application.Features.Movies.Commands.Create;

public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
{
    public CreateMovieCommandValidator()
    {
        RuleFor(c => c.MovieUrl).NotEmpty();
    }
}