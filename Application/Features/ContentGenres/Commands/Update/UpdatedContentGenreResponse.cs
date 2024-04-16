using Core.Application.Responses;

namespace Application.Features.ContentGenres.Commands.Update;

public class UpdatedContentGenreResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int GenreId { get; set; }
}