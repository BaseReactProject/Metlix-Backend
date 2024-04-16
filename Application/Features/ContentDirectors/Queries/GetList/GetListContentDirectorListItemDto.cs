using Core.Application.Dtos;

namespace Application.Features.ContentDirectors.Queries.GetList;

public class GetListContentDirectorListItemDto : IDto
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }
}