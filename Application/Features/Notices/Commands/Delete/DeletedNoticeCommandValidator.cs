using FluentValidation;

namespace Application.Features.Notices.Commands.Delete;

public class DeleteNoticeCommandValidator : AbstractValidator<DeleteNoticeCommand>
{
    public DeleteNoticeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}