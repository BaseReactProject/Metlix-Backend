using Core.Application.Responses;

namespace Application.Features.ContentGenres.Commands.Create;

public class CreatedContentGenreResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int GenreId { get; set; }
}