using Core.Application.Responses;

namespace Application.Features.ContentGenres.Commands.Delete;

public class DeletedContentGenreResponse : IResponse
{
    public int Id { get; set; }
}