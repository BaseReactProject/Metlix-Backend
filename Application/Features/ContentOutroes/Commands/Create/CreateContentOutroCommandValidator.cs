using FluentValidation;

namespace Application.Features.ContentOutroes.Commands.Create;

public class CreateContentOutroCommandValidator : AbstractValidator<CreateContentOutroCommand>
{
    public CreateContentOutroCommandValidator()
    {
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.StartTime).NotEmpty();
        RuleFor(c => c.EndTime).NotEmpty();
    }
}