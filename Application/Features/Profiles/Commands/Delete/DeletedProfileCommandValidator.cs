using FluentValidation;

namespace Application.Features.Profiles.Commands.Delete;

public class DeleteProfileCommandValidator : AbstractValidator<DeleteProfileCommand>
{
    public DeleteProfileCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}