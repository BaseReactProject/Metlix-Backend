using Core.Application.Responses;

namespace Application.Features.Genres.Commands.Delete;

public class DeletedGenreResponse : IResponse
{
    public int Id { get; set; }
}