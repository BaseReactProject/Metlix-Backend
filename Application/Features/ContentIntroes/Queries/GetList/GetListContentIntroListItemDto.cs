using Core.Application.Dtos;

namespace Application.Features.ContentIntroes.Queries.GetList;

public class GetListContentIntroListItemDto : IDto
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}