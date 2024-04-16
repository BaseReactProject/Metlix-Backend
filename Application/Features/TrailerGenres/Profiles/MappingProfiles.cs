using Application.Features.TrailerGenres.Commands.Create;
using Application.Features.TrailerGenres.Commands.Delete;
using Application.Features.TrailerGenres.Commands.Update;
using Application.Features.TrailerGenres.Queries.GetById;
using Application.Features.TrailerGenres.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.TrailerGenres.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<TrailerGenre, CreateTrailerGenreCommand>().ReverseMap();
        CreateMap<TrailerGenre, CreatedTrailerGenreResponse>().ReverseMap();
        CreateMap<TrailerGenre, UpdateTrailerGenreCommand>().ReverseMap();
        CreateMap<TrailerGenre, UpdatedTrailerGenreResponse>().ReverseMap();
        CreateMap<TrailerGenre, DeleteTrailerGenreCommand>().ReverseMap();
        CreateMap<TrailerGenre, DeletedTrailerGenreResponse>().ReverseMap();
        CreateMap<TrailerGenre, GetByIdTrailerGenreResponse>().ReverseMap();
        CreateMap<TrailerGenre, GetListTrailerGenreListItemDto>().ReverseMap();
        CreateMap<IPaginate<TrailerGenre>, GetListResponse<GetListTrailerGenreListItemDto>>().ReverseMap();
    }
}