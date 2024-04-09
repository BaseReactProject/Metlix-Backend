using FluentValidation;

namespace Application.Features.Qualities.Commands.Delete;

public class DeleteQualityCommandValidator : AbstractValidator<DeleteQualityCommand>
{
    public DeleteQualityCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}