using Core.Application.Responses;

namespace Application.Features.Trailers.Commands.Delete;

public class DeletedTrailerResponse : IResponse
{
    public int Id { get; set; }
}