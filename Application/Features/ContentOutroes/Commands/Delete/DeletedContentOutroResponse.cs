using Core.Application.Responses;

namespace Application.Features.ContentOutroes.Commands.Delete;

public class DeletedContentOutroResponse : IResponse
{
    public int Id { get; set; }
}