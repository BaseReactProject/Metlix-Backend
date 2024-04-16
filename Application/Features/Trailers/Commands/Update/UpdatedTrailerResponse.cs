using Core.Application.Responses;

namespace Application.Features.Trailers.Commands.Update;

public class UpdatedTrailerResponse : IResponse
{
    public int Id { get; set; }
    public string TrailerUrl { get; set; }
}