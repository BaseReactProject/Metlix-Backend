using Core.Application.Dtos;

namespace Application.Features.Contents.Queries.GetList;

public class GetListContentListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int MovieId { get; set; }
    public string ThumbnailUrl { get; set; }
    public float Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string AgeLimit { get; set; }
    public string Description { get; set; }
}