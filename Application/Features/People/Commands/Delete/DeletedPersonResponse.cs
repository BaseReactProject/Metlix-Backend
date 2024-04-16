using Core.Application.Responses;

namespace Application.Features.People.Commands.Delete;

public class DeletedPersonResponse : IResponse
{
    public int Id { get; set; }
}