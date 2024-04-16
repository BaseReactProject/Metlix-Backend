using FluentValidation;

namespace Application.Features.Trailers.Commands.Delete;

public class DeleteTrailerCommandValidator : AbstractValidator<DeleteTrailerCommand>
{
    public DeleteTrailerCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}