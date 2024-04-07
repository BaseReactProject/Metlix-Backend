using FluentValidation;


namespace Application.Features.Auth.Commands.UpdatePassword;

public class UpdatePasswordValidator:AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordValidator()
    {
        RuleFor(p=>p.FakeId).NotEmpty().MinimumLength(24).MaximumLength(26).WithMessage("Geçersiz Id");
    }
}
