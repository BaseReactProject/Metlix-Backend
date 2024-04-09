using Application.Features.Profiles.Commands.Create;
using Application.Features.Profiles.Commands.Delete;
using Application.Features.Profiles.Commands.Update;
using Application.Features.Profiles.Queries.GetById;
using Application.Features.Profiles.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Profiles.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Profile, CreateProfileCommand>().ReverseMap();
        CreateMap<Profile, CreatedProfileResponse>().ReverseMap();
        CreateMap<Profile, UpdateProfileCommand>().ReverseMap();
        CreateMap<Profile, UpdatedProfileResponse>().ReverseMap();
        CreateMap<Profile, DeleteProfileCommand>().ReverseMap();
        CreateMap<Profile, DeletedProfileResponse>().ReverseMap();
        CreateMap<Profile, GetByIdProfileResponse>().ReverseMap();
        CreateMap<Profile, GetListProfileListItemDto>().ReverseMap();
        CreateMap<IPaginate<Profile>, GetListResponse<GetListProfileListItemDto>>().ReverseMap();
    }
}