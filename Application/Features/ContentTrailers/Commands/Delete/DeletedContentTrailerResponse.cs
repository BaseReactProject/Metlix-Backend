using Core.Application.Responses;

namespace Application.Features.ContentTrailers.Commands.Delete;

public class DeletedContentTrailerResponse : IResponse
{
    public int Id { get; set; }
}