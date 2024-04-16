using Core.Application.Dtos;

namespace Application.Features.ContentCategories.Queries.GetList;

public class GetListContentCategoryListItemDto : IDto
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int GenreId { get; set; }
}