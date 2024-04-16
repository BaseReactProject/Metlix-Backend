using FluentValidation;

namespace Application.Features.ContentNotices.Commands.Create;

public class CreateContentNoticeCommandValidator : AbstractValidator<CreateContentNoticeCommand>
{
    public CreateContentNoticeCommandValidator()
    {
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.NoticeId).NotEmpty();
    }
}