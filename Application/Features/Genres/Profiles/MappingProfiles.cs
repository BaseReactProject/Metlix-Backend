using Application.Features.Genres.Commands.Create;
using Application.Features.Genres.Commands.Delete;
using Application.Features.Genres.Commands.Update;
using Application.Features.Genres.Queries.GetById;
using Application.Features.Genres.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Genres.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<Genre, CreateGenreCommand>().ReverseMap();
        CreateMap<Genre, CreatedGenreResponse>().ReverseMap();
        CreateMap<Genre, UpdateGenreCommand>().ReverseMap();
        CreateMap<Genre, UpdatedGenreResponse>().ReverseMap();
        CreateMap<Genre, DeleteGenreCommand>().ReverseMap();
        CreateMap<Genre, DeletedGenreResponse>().ReverseMap();
        CreateMap<Genre, GetByIdGenreResponse>().ReverseMap();
        CreateMap<Genre, GetListGenreListItemDto>().ReverseMap();
        CreateMap<IPaginate<Genre>, GetListResponse<GetListGenreListItemDto>>().ReverseMap();
    }
}