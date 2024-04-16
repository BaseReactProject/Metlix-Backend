using Core.Application.Responses;

namespace Application.Features.Notices.Queries.GetById;

public class GetByIdNoticeResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}