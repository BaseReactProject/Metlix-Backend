using Application.Features.ContentGenres.Commands.Create;
using Application.Features.ContentGenres.Commands.Delete;
using Application.Features.ContentGenres.Commands.Update;
using Application.Features.ContentGenres.Queries.GetById;
using Application.Features.ContentGenres.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContentGenres.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentGenre, CreateContentGenreCommand>().ReverseMap();
        CreateMap<ContentGenre, CreatedContentGenreResponse>().ReverseMap();
        CreateMap<ContentGenre, UpdateContentGenreCommand>().ReverseMap();
        CreateMap<ContentGenre, UpdatedContentGenreResponse>().ReverseMap();
        CreateMap<ContentGenre, DeleteContentGenreCommand>().ReverseMap();
        CreateMap<ContentGenre, DeletedContentGenreResponse>().ReverseMap();
        CreateMap<ContentGenre, GetByIdContentGenreResponse>().ReverseMap();
        CreateMap<ContentGenre, GetListContentGenreListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentGenre>, GetListResponse<GetListContentGenreListItemDto>>().ReverseMap();
    }
}