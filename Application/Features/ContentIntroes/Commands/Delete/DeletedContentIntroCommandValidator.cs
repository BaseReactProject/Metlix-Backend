using FluentValidation;

namespace Application.Features.ContentIntroes.Commands.Delete;

public class DeleteContentIntroCommandValidator : AbstractValidator<DeleteContentIntroCommand>
{
    public DeleteContentIntroCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}