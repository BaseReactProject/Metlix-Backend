using Core.Application.Dtos;

namespace Application.Features.FakeEntities.Queries.GetList;

public class GetListFakeEntityListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BrandId { get; set; }
}