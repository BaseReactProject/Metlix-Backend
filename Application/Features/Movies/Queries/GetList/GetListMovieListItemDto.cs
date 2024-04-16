using Core.Application.Dtos;

namespace Application.Features.Movies.Queries.GetList;

public class GetListMovieListItemDto : IDto
{
    public int Id { get; set; }
    public string MovieUrl { get; set; }
}