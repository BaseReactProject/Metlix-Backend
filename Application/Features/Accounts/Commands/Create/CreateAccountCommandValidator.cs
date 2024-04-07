using FluentValidation;

namespace Application.Features.Accounts.Commands.Create;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(c => c.FakeId).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
    }
}