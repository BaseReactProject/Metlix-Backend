using Core.Application.Dtos;

namespace Application.Features.AccountProfiles.Queries.GetList;

public class GetListAccountProfileListItemDto : IDto
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int ProfileId { get; set; }
}