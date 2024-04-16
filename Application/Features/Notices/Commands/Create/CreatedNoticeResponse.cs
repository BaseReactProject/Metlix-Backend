using Core.Application.Responses;

namespace Application.Features.Notices.Commands.Create;

public class CreatedNoticeResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}