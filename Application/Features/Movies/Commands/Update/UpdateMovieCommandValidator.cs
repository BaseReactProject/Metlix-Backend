using FluentValidation;

namespace Application.Features.Movies.Commands.Update;

public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
{
    public UpdateMovieCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.MovieUrl).NotEmpty();
    }
}