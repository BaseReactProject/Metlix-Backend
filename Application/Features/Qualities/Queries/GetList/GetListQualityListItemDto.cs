using Core.Application.Dtos;
using Nest;

namespace Application.Features.Qualities.Queries.GetList;

public class GetListQualityListItemDto : IDto
{
    public GetListQualityListItemDto()
    {
        Name = string.Empty;
        Value = 0;
    }

    public GetListQualityListItemDto(int �d, string name, int value)
    {
        Id = �d;
        Name = name;
        Value = value;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
}