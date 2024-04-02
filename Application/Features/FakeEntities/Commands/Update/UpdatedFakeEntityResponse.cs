using Core.Application.Responses;

namespace Application.Features.FakeEntities.Commands.Update;

public class UpdatedFakeEntityResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BrandId { get; set; }
}