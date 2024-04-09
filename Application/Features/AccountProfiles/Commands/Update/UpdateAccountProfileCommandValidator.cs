using FluentValidation;

namespace Application.Features.AccountProfiles.Commands.Update;

public class UpdateAccountProfileCommandValidator : AbstractValidator<UpdateAccountProfileCommand>
{
    public UpdateAccountProfileCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.AccountId).NotEmpty();
        RuleFor(c => c.ProfileId).NotEmpty();
    }
}