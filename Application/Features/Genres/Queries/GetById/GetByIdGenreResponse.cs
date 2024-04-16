using Core.Application.Responses;

namespace Application.Features.Genres.Queries.GetById;

public class GetByIdGenreResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}