using Core.Application.Responses;

namespace Application.Features.ContentIntroes.Commands.Create;

public class CreatedContentIntroResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}