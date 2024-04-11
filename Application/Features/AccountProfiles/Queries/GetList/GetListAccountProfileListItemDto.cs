using Application.Features.Profiles.Queries.GetList;
using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.AccountProfiles.Queries.GetList;

public class GetListAccountProfileListItemDto : IDto
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int ProfileId { get; set; }
    public GetListProfileListItemDto? Profile { get; set; }
}