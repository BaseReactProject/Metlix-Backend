using FluentValidation;

namespace Application.Features.ContentTrailers.Commands.Update;

public class UpdateContentTrailerCommandValidator : AbstractValidator<UpdateContentTrailerCommand>
{
    public UpdateContentTrailerCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.TrailerId).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
    }
}