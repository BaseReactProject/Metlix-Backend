using Core.Application.Dtos;

namespace Application.Features.Images.Queries.GetList;

public class GetListImageListItemDto : IDto
{
    public GetListImageListItemDto()
    {
        ImageUrl = string.Empty;
    }

    public GetListImageListItemDto(int ýd, string ýmageUrl)
    {
        Id = ýd;
        ImageUrl = ýmageUrl;
    }

    public int Id { get; set; }
    public string ImageUrl { get; set; }
}