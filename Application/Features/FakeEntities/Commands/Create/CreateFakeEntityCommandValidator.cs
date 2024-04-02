using FluentValidation;

namespace Application.Features.FakeEntities.Commands.Create;

public class CreateFakeEntityCommandValidator : AbstractValidator<CreateFakeEntityCommand>
{
    public CreateFakeEntityCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.BrandId).NotEmpty();
    }
}