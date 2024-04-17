using Core.Application.Responses;

namespace Application.Features.ContentIntroes.Commands.Delete;

public class DeletedContentIntroResponse : IResponse
{
    public int Id { get; set; }
}