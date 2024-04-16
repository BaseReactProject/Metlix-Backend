using Core.Application.Responses;

namespace Application.Features.ContentNotices.Commands.Delete;

public class DeletedContentNoticeResponse : IResponse
{
    public int Id { get; set; }
}