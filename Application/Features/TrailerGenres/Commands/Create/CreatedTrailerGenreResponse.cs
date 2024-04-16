using Core.Application.Responses;

namespace Application.Features.TrailerGenres.Commands.Create;

public class CreatedTrailerGenreResponse : IResponse
{
    public int Id { get; set; }
    public int TrailerId { get; set; }
    public int GenreId { get; set; }
}