using FluentValidation;

namespace Application.Features.ContentNotices.Commands.Update;

public class UpdateContentNoticeCommandValidator : AbstractValidator<UpdateContentNoticeCommand>
{
    public UpdateContentNoticeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.NoticeId).NotEmpty();
    }
}