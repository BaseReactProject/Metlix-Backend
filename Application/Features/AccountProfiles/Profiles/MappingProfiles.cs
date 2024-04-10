using Application.Features.AccountProfiles.Commands.Create;
using Application.Features.AccountProfiles.Commands.Delete;
using Application.Features.AccountProfiles.Commands.Update;
using Application.Features.AccountProfiles.Queries.GetById;
using Application.Features.AccountProfiles.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.AccountProfiles.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<AccountProfile, CreateAccountProfileCommand>().ReverseMap();
        CreateMap<AccountProfile, CreatedAccountProfileResponse>().ReverseMap();
        CreateMap<AccountProfile, UpdateAccountProfileCommand>().ReverseMap();
        CreateMap<AccountProfile, UpdatedAccountProfileResponse>().ReverseMap();
        CreateMap<AccountProfile, DeleteAccountProfileCommand>().ReverseMap();
        CreateMap<AccountProfile, DeletedAccountProfileResponse>().ReverseMap();
        CreateMap<AccountProfile, GetByIdAccountProfileResponse>().ReverseMap();
        CreateMap<AccountProfile, GetListAccountProfileListItemDto>().ReverseMap();
        CreateMap<IPaginate<AccountProfile>, GetListResponse<GetListAccountProfileListItemDto>>().ReverseMap();
    }
}