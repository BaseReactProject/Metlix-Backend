using FluentValidation;

namespace Application.Features.Images.Commands.Create;

public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
{
    public CreateImageCommandValidator()
    {
        RuleFor(c => c.ImagePath).NotEmpty();
    }
}