using Core.Application.Responses;

namespace Application.Features.ContentOutroes.Commands.Update;

public class UpdatedContentOutroResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}