using Core.Application.Responses;

namespace Application.Features.ContentActors.Commands.Delete;

public class DeletedContentActorResponse : IResponse
{
    public int Id { get; set; }
}