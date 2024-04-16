using Core.Application.Dtos;

namespace Application.Features.ContentGenres.Queries.GetList;

public class GetListContentGenreListItemDto : IDto
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int GenreId { get; set; }
}