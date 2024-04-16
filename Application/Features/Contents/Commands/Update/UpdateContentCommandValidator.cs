using FluentValidation;

namespace Application.Features.Contents.Commands.Update;

public class UpdateContentCommandValidator : AbstractValidator<UpdateContentCommand>
{
    public UpdateContentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.MovieId).NotEmpty();
        RuleFor(c => c.ThumbnailUrl).NotEmpty();
        RuleFor(c => c.Duration).NotEmpty();
        RuleFor(c => c.ReleaseDate).NotEmpty();
        RuleFor(c => c.AgeLimit).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
    }
}