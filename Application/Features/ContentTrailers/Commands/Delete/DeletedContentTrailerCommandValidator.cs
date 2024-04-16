using FluentValidation;

namespace Application.Features.ContentTrailers.Commands.Delete;

public class DeleteContentTrailerCommandValidator : AbstractValidator<DeleteContentTrailerCommand>
{
    public DeleteContentTrailerCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}