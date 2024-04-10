

using FluentValidation;

namespace Application.Features.Auth.Commands.ProfileLogin;

public class ProfileLoginCommandValidatior : AbstractValidator<ProfileLoginCommand>
{
    public ProfileLoginCommandValidatior()
    {
        RuleFor(plc => plc.ProfileId).NotEmpty();
        RuleFor(plc => plc.Password).NotEmpty();
        RuleFor(plc=>plc.Password).MaximumLength(4).WithMessage("Password Have to 4 Characters");
    }
}
