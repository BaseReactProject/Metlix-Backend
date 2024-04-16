using Core.Application.Responses;

namespace Application.Features.Notices.Commands.Delete;

public class DeletedNoticeResponse : IResponse
{
    public int Id { get; set; }
}