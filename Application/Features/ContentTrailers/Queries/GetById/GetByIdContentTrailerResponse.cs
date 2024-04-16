using Core.Application.Responses;

namespace Application.Features.ContentTrailers.Queries.GetById;

public class GetByIdContentTrailerResponse : IResponse
{
    public int Id { get; set; }
    public int TrailerId { get; set; }
    public int ContentId { get; set; }
}