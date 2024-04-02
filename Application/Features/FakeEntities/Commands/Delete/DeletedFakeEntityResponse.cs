using Core.Application.Responses;

namespace Application.Features.FakeEntities.Commands.Delete;

public class DeletedFakeEntityResponse : IResponse
{
    public int Id { get; set; }
}