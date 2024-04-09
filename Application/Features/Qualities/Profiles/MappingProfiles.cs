using Application.Features.Qualities.Commands.Create;
using Application.Features.Qualities.Commands.Delete;
using Application.Features.Qualities.Commands.Update;
using Application.Features.Qualities.Queries.GetById;
using Application.Features.Qualities.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Qualities.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Quality, CreateQualityCommand>().ReverseMap();
        CreateMap<Quality, CreatedQualityResponse>().ReverseMap();
        CreateMap<Quality, UpdateQualityCommand>().ReverseMap();
        CreateMap<Quality, UpdatedQualityResponse>().ReverseMap();
        CreateMap<Quality, DeleteQualityCommand>().ReverseMap();
        CreateMap<Quality, DeletedQualityResponse>().ReverseMap();
        CreateMap<Quality, GetByIdQualityResponse>().ReverseMap();
        CreateMap<Quality, GetListQualityListItemDto>().ReverseMap();
        CreateMap<IPaginate<Quality>, GetListResponse<GetListQualityListItemDto>>().ReverseMap();
    }
}