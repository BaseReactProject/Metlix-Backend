using Core.Application.Dtos;

namespace Application.Features.ContentNotices.Queries.GetList;

public class GetListContentNoticeListItemDto : IDto
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int NoticeId { get; set; }
}