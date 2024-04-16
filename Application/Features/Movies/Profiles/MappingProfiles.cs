using Application.Features.Movies.Commands.Create;
using Application.Features.Movies.Commands.Delete;
using Application.Features.Movies.Commands.Update;
using Application.Features.Movies.Queries.GetById;
using Application.Features.Movies.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Movies.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<Movie, CreateMovieCommand>().ReverseMap();
        CreateMap<Movie, CreatedMovieResponse>().ReverseMap();
        CreateMap<Movie, UpdateMovieCommand>().ReverseMap();
        CreateMap<Movie, UpdatedMovieResponse>().ReverseMap();
        CreateMap<Movie, DeleteMovieCommand>().ReverseMap();
        CreateMap<Movie, DeletedMovieResponse>().ReverseMap();
        CreateMap<Movie, GetByIdMovieResponse>().ReverseMap();
        CreateMap<Movie, GetListMovieListItemDto>().ReverseMap();
        CreateMap<IPaginate<Movie>, GetListResponse<GetListMovieListItemDto>>().ReverseMap();
    }
}