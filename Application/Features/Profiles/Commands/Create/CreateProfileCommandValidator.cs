using FluentValidation;

namespace Application.Features.Profiles.Commands.Create;

public class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
{
    public CreateProfileCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2).MaximumLength(30).WithMessage("30 dan az 2 den fazla olmalý profil adý");
        RuleFor(c => c.Password).NotEmpty().Length(4).WithMessage("4 haneli olmalý");
    }
}