using Core.Application.Responses;

namespace Application.Features.Genres.Commands.Update;

public class UpdatedGenreResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}