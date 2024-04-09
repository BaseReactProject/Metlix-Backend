using Core.Application.Dtos;

namespace Application.Features.Images.Queries.GetList;

public class GetListImageListItemDto : IDto
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
}