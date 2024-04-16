using Core.Application.Responses;

namespace Application.Features.Movies.Queries.GetById;

public class GetByIdMovieResponse : IResponse
{
    public int Id { get; set; }
    public string MovieUrl { get; set; }
}