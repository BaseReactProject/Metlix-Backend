using Application.Features.ContentTrailers.Commands.Create;
using Application.Features.ContentTrailers.Commands.Delete;
using Application.Features.ContentTrailers.Commands.Update;
using Application.Features.ContentTrailers.Queries.GetById;
using Application.Features.ContentTrailers.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContentTrailers.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentTrailer, CreateContentTrailerCommand>().ReverseMap();
        CreateMap<ContentTrailer, CreatedContentTrailerResponse>().ReverseMap();
        CreateMap<ContentTrailer, UpdateContentTrailerCommand>().ReverseMap();
        CreateMap<ContentTrailer, UpdatedContentTrailerResponse>().ReverseMap();
        CreateMap<ContentTrailer, DeleteContentTrailerCommand>().ReverseMap();
        CreateMap<ContentTrailer, DeletedContentTrailerResponse>().ReverseMap();
        CreateMap<ContentTrailer, GetByIdContentTrailerResponse>().ReverseMap();
        CreateMap<ContentTrailer, GetListContentTrailerListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentTrailer>, GetListResponse<GetListContentTrailerListItemDto>>().ReverseMap();
    }
}