using FluentValidation;

namespace Application.Features.ContentCategories.Commands.Create;

public class CreateContentCategoryCommandValidator : AbstractValidator<CreateContentCategoryCommand>
{
    public CreateContentCategoryCommandValidator()
    {
        RuleFor(c => c.ContentId).NotEmpty();
        RuleFor(c => c.GenreId).NotEmpty();
    }
}