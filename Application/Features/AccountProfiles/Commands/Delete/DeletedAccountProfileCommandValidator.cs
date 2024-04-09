using FluentValidation;

namespace Application.Features.AccountProfiles.Commands.Delete;

public class DeleteAccountProfileCommandValidator : AbstractValidator<DeleteAccountProfileCommand>
{
    public DeleteAccountProfileCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}