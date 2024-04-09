using FluentValidation;

namespace Application.Features.AccountCreditCards.Commands.Update;

public class UpdateAccountCreditCardCommandValidator : AbstractValidator<UpdateAccountCreditCardCommand>
{
    public UpdateAccountCreditCardCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.AccountId).NotEmpty();
    }
}