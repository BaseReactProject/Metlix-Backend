using FluentValidation;

namespace Application.Features.ContentIntroes.Commands.Create;

public class CreateContentIntroCommandValidator : AbstractValidator<CreateContentIntroCommand>
{
    public CreateContentIntroCommandValidator()
    {
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.StartTime).NotEmpty();
        RuleFor(c => c.EndTime).NotEmpty();
    }
}