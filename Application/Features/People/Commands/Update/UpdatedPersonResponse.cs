using Core.Application.Responses;

namespace Application.Features.People.Commands.Update;

public class UpdatedPersonResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}