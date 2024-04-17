using Application.Features.ContentIntroes.Commands.Create;
using Application.Features.ContentIntroes.Commands.Delete;
using Application.Features.ContentIntroes.Commands.Update;
using Application.Features.ContentIntroes.Queries.GetById;
using Application.Features.ContentIntroes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContentIntroes.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentIntro, CreateContentIntroCommand>().ReverseMap();
        CreateMap<ContentIntro, CreatedContentIntroResponse>().ReverseMap();
        CreateMap<ContentIntro, UpdateContentIntroCommand>().ReverseMap();
        CreateMap<ContentIntro, UpdatedContentIntroResponse>().ReverseMap();
        CreateMap<ContentIntro, DeleteContentIntroCommand>().ReverseMap();
        CreateMap<ContentIntro, DeletedContentIntroResponse>().ReverseMap();
        CreateMap<ContentIntro, GetByIdContentIntroResponse>().ReverseMap();
        CreateMap<ContentIntro, GetListContentIntroListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentIntro>, GetListResponse<GetListContentIntroListItemDto>>().ReverseMap();
    }
}