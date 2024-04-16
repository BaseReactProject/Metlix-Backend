using Core.Application.Responses;

namespace Application.Features.ContentDirectors.Commands.Create;

public class CreatedContentDirectorResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }
}