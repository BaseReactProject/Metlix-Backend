using FluentValidation;

namespace Application.Features.AccountProfiles.Commands.Create;

public class CreateAccountProfileCommandValidator : AbstractValidator<CreateAccountProfileCommand>
{
    public CreateAccountProfileCommandValidator()
    {
        RuleFor(c => c.AccountId).NotEmpty();
        RuleFor(c => c.ProfileId).NotEmpty();
    }
}