using Core.Application.Responses;

namespace Application.Features.Trailers.Queries.GetById;

public class GetByIdTrailerResponse : IResponse
{
    public int Id { get; set; }
    public string TrailerUrl { get; set; }
}