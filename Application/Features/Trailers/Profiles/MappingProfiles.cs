using Application.Features.Trailers.Commands.Create;
using Application.Features.Trailers.Commands.Delete;
using Application.Features.Trailers.Commands.Update;
using Application.Features.Trailers.Queries.GetById;
using Application.Features.Trailers.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Trailers.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<Trailer, CreateTrailerCommand>().ReverseMap();
        CreateMap<Trailer, CreatedTrailerResponse>().ReverseMap();
        CreateMap<Trailer, UpdateTrailerCommand>().ReverseMap();
        CreateMap<Trailer, UpdatedTrailerResponse>().ReverseMap();
        CreateMap<Trailer, DeleteTrailerCommand>().ReverseMap();
        CreateMap<Trailer, DeletedTrailerResponse>().ReverseMap();
        CreateMap<Trailer, GetByIdTrailerResponse>().ReverseMap();
        CreateMap<Trailer, GetListTrailerListItemDto>().ReverseMap();
        CreateMap<IPaginate<Trailer>, GetListResponse<GetListTrailerListItemDto>>().ReverseMap();
    }
}