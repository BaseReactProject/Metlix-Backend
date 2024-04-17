using FluentValidation;

namespace Application.Features.ContentIntroes.Commands.Update;

public class UpdateContentIntroCommandValidator : AbstractValidator<UpdateContentIntroCommand>
{
    public UpdateContentIntroCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.StartTime).NotEmpty();
        RuleFor(c => c.EndTime).NotEmpty();
    }
}