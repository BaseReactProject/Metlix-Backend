using Core.Application.Responses;

namespace Application.Features.ContentNotices.Queries.GetById;

public class GetByIdContentNoticeResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int NoticeId { get; set; }
}