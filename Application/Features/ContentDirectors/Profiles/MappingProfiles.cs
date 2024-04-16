using Application.Features.ContentDirectors.Commands.Create;
using Application.Features.ContentDirectors.Commands.Delete;
using Application.Features.ContentDirectors.Commands.Update;
using Application.Features.ContentDirectors.Queries.GetById;
using Application.Features.ContentDirectors.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContentDirectors.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentDirector, CreateContentDirectorCommand>().ReverseMap();
        CreateMap<ContentDirector, CreatedContentDirectorResponse>().ReverseMap();
        CreateMap<ContentDirector, UpdateContentDirectorCommand>().ReverseMap();
        CreateMap<ContentDirector, UpdatedContentDirectorResponse>().ReverseMap();
        CreateMap<ContentDirector, DeleteContentDirectorCommand>().ReverseMap();
        CreateMap<ContentDirector, DeletedContentDirectorResponse>().ReverseMap();
        CreateMap<ContentDirector, GetByIdContentDirectorResponse>().ReverseMap();
        CreateMap<ContentDirector, GetListContentDirectorListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentDirector>, GetListResponse<GetListContentDirectorListItemDto>>().ReverseMap();
    }
}