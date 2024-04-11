using Application.Features.AccountProfiles.Queries.GetList;
using Application.Features.Profiles.Queries.GetList;
using Core.Application.Dtos;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.Accounts.Queries.GetList;

public class GetListAccountListItemDto : IDto
{
    public GetListAccountListItemDto()
    {
        FakeId = string.Empty;
        UserId = 0;
    }

    public GetListAccountListItemDto(int ýd, string fakeId, int userId)
    {
        Id = ýd;
        FakeId = fakeId;
        UserId = userId;
    }

    public int Id { get; set; }
    public string FakeId { get; set; }
    public int UserId { get; set; }
    public ICollection<GetListAccountProfileListItemDto>? AccountProfiles { get; set; } 
}