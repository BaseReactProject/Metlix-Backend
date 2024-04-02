using Core.Application.Responses;

namespace Application.Features.FakeEntities.Commands.Create;

public class CreatedFakeEntityResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BrandId { get; set; }
}