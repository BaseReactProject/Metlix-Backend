using FluentValidation;

namespace Application.Features.ContentNotices.Commands.Delete;

public class DeleteContentNoticeCommandValidator : AbstractValidator<DeleteContentNoticeCommand>
{
    public DeleteContentNoticeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}