
using Application.Features.AccountProfiles.Queries.GetList;
using Application.Features.Accounts.Queries.GetList;
using Application.Features.Auth.Commands.RevokeToken;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.Auth.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<RefreshToken, RevokedTokenResponse>().ReverseMap();
        CreateMap<Account, GetListAccountListItemDto>().ReverseMap();
        CreateMap<IPaginate<AccountProfile>, GetListResponse<GetListAccountListItemDto>>().ReverseMap();
        CreateMap<Account, GetListAccountListItemDto>()
         .ForMember(dest => dest.AccountProfiles, opt => opt.MapFrom(src => src.AccountProfiles.Select(ap=>ap.Profile).ToList()))
         ;
    }
}
