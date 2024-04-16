using FluentValidation;

namespace Application.Features.ContentTrailers.Commands.Create;

public class CreateContentTrailerCommandValidator : AbstractValidator<CreateContentTrailerCommand>
{
    public CreateContentTrailerCommandValidator()
    {
        RuleFor(c => c.TrailerId).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
    }
}