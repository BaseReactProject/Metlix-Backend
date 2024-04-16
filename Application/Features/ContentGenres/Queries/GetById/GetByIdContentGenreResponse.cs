using Core.Application.Responses;

namespace Application.Features.ContentGenres.Queries.GetById;

public class GetByIdContentGenreResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int GenreId { get; set; }
}