using FluentValidation;

namespace Application.Features.Notices.Commands.Create;

public class CreateNoticeCommandValidator : AbstractValidator<CreateNoticeCommand>
{
    public CreateNoticeCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}