using Core.Application.Responses;

namespace Application.Features.ContentIntroes.Queries.GetById;

public class GetByIdContentIntroResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}