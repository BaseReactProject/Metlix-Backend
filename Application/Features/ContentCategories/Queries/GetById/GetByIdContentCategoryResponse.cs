using Core.Application.Responses;

namespace Application.Features.ContentCategories.Queries.GetById;

public class GetByIdContentCategoryResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int GenreId { get; set; }
}