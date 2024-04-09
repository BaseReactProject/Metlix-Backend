using FluentValidation;

namespace Application.Features.AccountCreditCards.Commands.Create;

public class CreateAccountCreditCardCommandValidator : AbstractValidator<CreateAccountCreditCardCommand>
{
    public CreateAccountCreditCardCommandValidator()
    {
        RuleFor(c => c.AccountId).NotEmpty();
    }
}