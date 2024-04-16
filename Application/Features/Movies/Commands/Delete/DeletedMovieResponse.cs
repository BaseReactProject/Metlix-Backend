using Core.Application.Responses;

namespace Application.Features.Movies.Commands.Delete;

public class DeletedMovieResponse : IResponse
{
    public int Id { get; set; }
}