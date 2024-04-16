using Core.Application.Responses;

namespace Application.Features.ContentActors.Commands.Update;

public class UpdatedContentActorResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }
}