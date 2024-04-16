using FluentValidation;

namespace Application.Features.ContentScenarists.Commands.Delete;

public class DeleteContentScenaristCommandValidator : AbstractValidator<DeleteContentScenaristCommand>
{
    public DeleteContentScenaristCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}