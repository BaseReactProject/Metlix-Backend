using Core.Application.Dtos;

namespace Application.Features.Notices.Queries.GetList;

public class GetListNoticeListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}