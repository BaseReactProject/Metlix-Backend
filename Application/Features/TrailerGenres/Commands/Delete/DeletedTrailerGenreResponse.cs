using Core.Application.Responses;

namespace Application.Features.TrailerGenres.Commands.Delete;

public class DeletedTrailerGenreResponse : IResponse
{
    public int Id { get; set; }
}