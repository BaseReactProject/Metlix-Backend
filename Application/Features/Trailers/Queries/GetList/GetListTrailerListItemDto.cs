using Core.Application.Dtos;

namespace Application.Features.Trailers.Queries.GetList;

public class GetListTrailerListItemDto : IDto
{
    public int Id { get; set; }
    public string TrailerUrl { get; set; }
}