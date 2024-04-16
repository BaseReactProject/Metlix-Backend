using Core.Application.Responses;

namespace Application.Features.ContentScenarists.Queries.GetById;

public class GetByIdContentScenaristResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }
}