using Core.Application.Responses;

namespace Application.Features.Notices.Commands.Update;

public class UpdatedNoticeResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}