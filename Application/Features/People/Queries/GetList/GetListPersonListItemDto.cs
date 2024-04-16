using Core.Application.Dtos;

namespace Application.Features.People.Queries.GetList;

public class GetListPersonListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}