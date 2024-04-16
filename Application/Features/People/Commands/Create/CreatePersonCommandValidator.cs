using FluentValidation;

namespace Application.Features.People.Commands.Create;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}