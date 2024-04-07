using Core.Application.Dtos;

namespace Application.Features.Accounts.Queries.GetList;

public class GetListAccountListItemDto : IDto
{
    public int Id { get; set; }
    public string FakeId { get; set; }
    public int UserId { get; set; }
}