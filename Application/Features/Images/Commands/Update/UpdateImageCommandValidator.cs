using FluentValidation;

namespace Application.Features.Images.Commands.Update;

public class UpdateImageCommandValidator : AbstractValidator<UpdateImageCommand>
{
    public UpdateImageCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ImageUrl).NotEmpty();
    }
}