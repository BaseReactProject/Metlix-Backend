using Core.Application.Dtos;

namespace Application.Features.Genres.Queries.GetList;

public class GetListGenreListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}