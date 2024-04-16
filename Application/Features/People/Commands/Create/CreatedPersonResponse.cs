using Core.Application.Responses;

namespace Application.Features.People.Commands.Create;

public class CreatedPersonResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}