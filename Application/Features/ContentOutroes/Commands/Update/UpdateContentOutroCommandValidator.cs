using FluentValidation;

namespace Application.Features.ContentOutroes.Commands.Update;

public class UpdateContentOutroCommandValidator : AbstractValidator<UpdateContentOutroCommand>
{
    public UpdateContentOutroCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.StartTime).NotEmpty();
        RuleFor(c => c.EndTime).NotEmpty();
    }
}