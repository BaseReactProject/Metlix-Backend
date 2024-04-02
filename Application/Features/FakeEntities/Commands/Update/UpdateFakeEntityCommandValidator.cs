using FluentValidation;

namespace Application.Features.FakeEntities.Commands.Update;

public class UpdateFakeEntityCommandValidator : AbstractValidator<UpdateFakeEntityCommand>
{
    public UpdateFakeEntityCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.BrandId).NotEmpty();
    }
}