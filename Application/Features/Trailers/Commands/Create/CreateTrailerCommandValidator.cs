using FluentValidation;

namespace Application.Features.Trailers.Commands.Create;

public class CreateTrailerCommandValidator : AbstractValidator<CreateTrailerCommand>
{
    public CreateTrailerCommandValidator()
    {
        RuleFor(c => c.TrailerUrl).NotEmpty();
    }
}