using Core.Application.Responses;

namespace Application.Features.ContentActors.Commands.Create;

public class CreatedContentActorResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }
}