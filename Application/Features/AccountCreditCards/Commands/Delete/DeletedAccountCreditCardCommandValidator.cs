using FluentValidation;

namespace Application.Features.AccountCreditCards.Commands.Delete;

public class DeleteAccountCreditCardCommandValidator : AbstractValidator<DeleteAccountCreditCardCommand>
{
    public DeleteAccountCreditCardCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}