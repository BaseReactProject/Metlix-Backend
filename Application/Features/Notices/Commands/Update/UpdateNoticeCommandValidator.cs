using FluentValidation;

namespace Application.Features.Notices.Commands.Update;

public class UpdateNoticeCommandValidator : AbstractValidator<UpdateNoticeCommand>
{
    public UpdateNoticeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}