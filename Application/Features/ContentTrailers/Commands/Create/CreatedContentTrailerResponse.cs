using Core.Application.Responses;

namespace Application.Features.ContentTrailers.Commands.Create;

public class CreatedContentTrailerResponse : IResponse
{
    public int Id { get; set; }
    public int TrailerId { get; set; }
    public int ContentId { get; set; }
}