using Core.Application.Responses;

namespace Application.Features.FakeEntities.Queries.GetById;

public class GetByIdFakeEntityResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BrandId { get; set; }
}