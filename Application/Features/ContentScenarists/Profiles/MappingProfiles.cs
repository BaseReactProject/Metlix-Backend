using Application.Features.ContentScenarists.Commands.Create;
using Application.Features.ContentScenarists.Commands.Delete;
using Application.Features.ContentScenarists.Commands.Update;
using Application.Features.ContentScenarists.Queries.GetById;
using Application.Features.ContentScenarists.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContentScenarists.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentScenarist, CreateContentScenaristCommand>().ReverseMap();
        CreateMap<ContentScenarist, CreatedContentScenaristResponse>().ReverseMap();
        CreateMap<ContentScenarist, UpdateContentScenaristCommand>().ReverseMap();
        CreateMap<ContentScenarist, UpdatedContentScenaristResponse>().ReverseMap();
        CreateMap<ContentScenarist, DeleteContentScenaristCommand>().ReverseMap();
        CreateMap<ContentScenarist, DeletedContentScenaristResponse>().ReverseMap();
        CreateMap<ContentScenarist, GetByIdContentScenaristResponse>().ReverseMap();
        CreateMap<ContentScenarist, GetListContentScenaristListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentScenarist>, GetListResponse<GetListContentScenaristListItemDto>>().ReverseMap();
    }
}