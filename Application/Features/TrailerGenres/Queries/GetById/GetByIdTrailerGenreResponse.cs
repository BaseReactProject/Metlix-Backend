using Core.Application.Responses;

namespace Application.Features.TrailerGenres.Queries.GetById;

public class GetByIdTrailerGenreResponse : IResponse
{
    public int Id { get; set; }
    public int TrailerId { get; set; }
    public int GenreId { get; set; }
}