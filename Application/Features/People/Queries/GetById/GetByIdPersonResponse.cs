using Core.Application.Responses;

namespace Application.Features.People.Queries.GetById;

public class GetByIdPersonResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}