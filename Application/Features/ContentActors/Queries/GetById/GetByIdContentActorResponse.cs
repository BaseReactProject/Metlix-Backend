using Core.Application.Responses;

namespace Application.Features.ContentActors.Queries.GetById;

public class GetByIdContentActorResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }
}