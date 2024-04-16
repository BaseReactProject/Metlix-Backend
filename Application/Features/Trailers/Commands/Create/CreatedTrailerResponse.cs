using Core.Application.Responses;

namespace Application.Features.Trailers.Commands.Create;

public class CreatedTrailerResponse : IResponse
{
    public int Id { get; set; }
    public string TrailerUrl { get; set; }
}