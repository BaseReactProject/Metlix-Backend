using Core.Application.Dtos;

namespace Application.Features.ContentTrailers.Queries.GetList;

public class GetListContentTrailerListItemDto : IDto
{
    public int Id { get; set; }
    public int TrailerId { get; set; }
    public int ContentId { get; set; }
}