using Core.Application.Responses;

namespace Application.Features.TrailerGenres.Commands.Update;

public class UpdatedTrailerGenreResponse : IResponse
{
    public int Id { get; set; }
    public int TrailerId { get; set; }
    public int GenreId { get; set; }
}