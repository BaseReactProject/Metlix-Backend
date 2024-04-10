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

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<Domain.Entities.Profile, CreateProfileCommand>().ReverseMap();
        CreateMap<Domain.Entities.Profile, CreatedProfileResponse>().ReverseMap();
        CreateMap<Domain.Entities.Profile, UpdateProfileCommand>().ReverseMap();
        CreateMap<Domain.Entities.Profile, UpdatedProfileResponse>().ReverseMap();
        CreateMap<Domain.Entities.Profile, DeleteProfileCommand>().ReverseMap();
        CreateMap<Domain.Entities.Profile, DeletedProfileResponse>().ReverseMap();
        CreateMap<Domain.Entities.Profile, GetByIdProfileResponse>().ReverseMap();
        CreateMap<Domain.Entities.Profile, GetListProfileListItemDto>().ReverseMap();
        CreateMap<IPaginate<Domain.Entities.Profile>, GetListResponse<GetListProfileListItemDto>>().ReverseMap();
    }
}