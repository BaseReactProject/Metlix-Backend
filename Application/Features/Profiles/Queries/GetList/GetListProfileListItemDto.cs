using Core.Application.Dtos;

namespace Application.Features.Profiles.Queries.GetList;

public class GetListProfileListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ImageId { get; set; }
}