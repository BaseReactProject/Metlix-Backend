using Core.Application.Dtos;

namespace Application.Features.ContentOutroes.Queries.GetList;

public class GetListContentOutroListItemDto : IDto
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}