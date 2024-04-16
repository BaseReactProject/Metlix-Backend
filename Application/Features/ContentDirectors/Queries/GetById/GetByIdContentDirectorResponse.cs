using Core.Application.Responses;

namespace Application.Features.ContentDirectors.Queries.GetById;

public class GetByIdContentDirectorResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }
}