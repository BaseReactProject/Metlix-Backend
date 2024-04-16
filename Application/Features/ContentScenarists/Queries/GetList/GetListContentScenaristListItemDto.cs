using Core.Application.Dtos;

namespace Application.Features.ContentScenarists.Queries.GetList;

public class GetListContentScenaristListItemDto : IDto
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }
}