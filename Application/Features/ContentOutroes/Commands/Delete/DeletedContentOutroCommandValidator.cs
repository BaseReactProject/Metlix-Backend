using FluentValidation;

namespace Application.Features.ContentOutroes.Commands.Delete;

public class DeleteContentOutroCommandValidator : AbstractValidator<DeleteContentOutroCommand>
{
    public DeleteContentOutroCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}