using Core.Application.Dtos;
using Nest;

namespace Application.Features.Profiles.Queries.GetList;

public class GetListProfileListItemDto : IDto
{
    public GetListProfileListItemDto()
    {
        Name = string.Empty;
        ImageId = 0;
    }

    public GetListProfileListItemDto(int �d, string name, int �mageId)
    {
        Id = �d;
        Name = name;
        ImageId = �mageId;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int ImageId { get; set; }
}