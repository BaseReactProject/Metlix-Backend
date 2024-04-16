using Core.Application.Responses;

namespace Application.Features.Contents.Commands.Delete;

public class DeletedContentResponse : IResponse
{
    public int Id { get; set; }
}