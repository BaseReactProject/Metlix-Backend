using Core.Application.Responses;

namespace Application.Features.ContentNotices.Commands.Create;

public class CreatedContentNoticeResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int NoticeId { get; set; }
}