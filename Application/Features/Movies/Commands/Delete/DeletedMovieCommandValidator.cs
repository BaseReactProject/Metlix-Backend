using FluentValidation;

namespace Application.Features.Movies.Commands.Delete;

public class DeleteMovieCommandValidator : AbstractValidator<DeleteMovieCommand>
{
    public DeleteMovieCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}