using Application.Features.ContentActors.Commands.Create;
using Application.Features.ContentActors.Commands.Delete;
using Application.Features.ContentActors.Commands.Update;
using Application.Features.ContentActors.Queries.GetById;
using Application.Features.ContentActors.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContentActors.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentActor, CreateContentActorCommand>().ReverseMap();
        CreateMap<ContentActor, CreatedContentActorResponse>().ReverseMap();
        CreateMap<ContentActor, UpdateContentActorCommand>().ReverseMap();
        CreateMap<ContentActor, UpdatedContentActorResponse>().ReverseMap();
        CreateMap<ContentActor, DeleteContentActorCommand>().ReverseMap();
        CreateMap<ContentActor, DeletedContentActorResponse>().ReverseMap();
        CreateMap<ContentActor, GetByIdContentActorResponse>().ReverseMap();
        CreateMap<ContentActor, GetListContentActorListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentActor>, GetListResponse<GetListContentActorListItemDto>>().ReverseMap();
    }
}