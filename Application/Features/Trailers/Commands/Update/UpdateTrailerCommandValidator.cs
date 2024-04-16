using FluentValidation;

namespace Application.Features.Trailers.Commands.Update;

public class UpdateTrailerCommandValidator : AbstractValidator<UpdateTrailerCommand>
{
    public UpdateTrailerCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.TrailerUrl).NotEmpty();
    }
}