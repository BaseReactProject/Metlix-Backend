using Core.Application.Responses;

namespace Application.Features.Genres.Commands.Create;

public class CreatedGenreResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}