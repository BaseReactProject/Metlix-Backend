using Core.Application.Responses;

namespace Application.Features.ContentDirectors.Commands.Update;

public class UpdatedContentDirectorResponse : IResponse
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }
}