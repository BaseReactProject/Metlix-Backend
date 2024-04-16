using Core.Application.Responses;

namespace Application.Features.ContentCategories.Commands.Update;

public class UpdatedContentCategoryResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int GenreId { get; set; }
}