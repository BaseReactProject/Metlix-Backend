using FluentValidation;

namespace Application.Features.Profiles.Commands.Create;

public class CreateProfileCommandValidator : AbstractValidator<CreateProfileCommand>
{
    public CreateProfileCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.ImageId).NotEmpty();
    }
}