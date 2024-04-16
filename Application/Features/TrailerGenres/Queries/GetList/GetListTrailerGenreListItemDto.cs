using Core.Application.Dtos;

namespace Application.Features.TrailerGenres.Queries.GetList;

public class GetListTrailerGenreListItemDto : IDto
{
    public int Id { get; set; }
    public int TrailerId { get; set; }
    public int GenreId { get; set; }
}