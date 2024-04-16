using Core.Application.Responses;

namespace Application.Features.Movies.Commands.Create;

public class CreatedMovieResponse : IResponse
{
    public int Id { get; set; }
    public string MovieUrl { get; set; }
}