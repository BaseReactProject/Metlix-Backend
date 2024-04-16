using Core.Application.Responses;

namespace Application.Features.Movies.Commands.Update;

public class UpdatedMovieResponse : IResponse
{
    public int Id { get; set; }
    public string MovieUrl { get; set; }
}