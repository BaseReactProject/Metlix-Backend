using Core.Application.Responses;

namespace Application.Features.ContentTrailers.Commands.Update;

public class UpdatedContentTrailerResponse : IResponse
{
    public int Id { get; set; }
    public int TrailerId { get; set; }
    public int ContentId { get; set; }
}