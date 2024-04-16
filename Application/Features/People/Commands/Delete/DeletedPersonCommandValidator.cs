using FluentValidation;

namespace Application.Features.People.Commands.Delete;

public class DeletePersonCommandValidator : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}