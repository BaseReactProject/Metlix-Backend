using Core.Application.Dtos;

namespace Application.Features.Qualities.Queries.GetList;

public class GetListQualityListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
}