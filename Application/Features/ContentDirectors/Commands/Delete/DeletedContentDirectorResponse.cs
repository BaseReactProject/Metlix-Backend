using Core.Application.Responses;

namespace Application.Features.ContentDirectors.Commands.Delete;

public class DeletedContentDirectorResponse : IResponse
{
    public int Id { get; set; }
}