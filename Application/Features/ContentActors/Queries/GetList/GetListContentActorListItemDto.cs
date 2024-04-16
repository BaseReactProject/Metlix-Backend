using Core.Application.Dtos;

namespace Application.Features.ContentActors.Queries.GetList;

public class GetListContentActorListItemDto : IDto
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }
}