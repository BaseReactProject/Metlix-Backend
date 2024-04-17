using Application.Features.ContentOutroes.Commands.Create;
using Application.Features.ContentOutroes.Commands.Delete;
using Application.Features.ContentOutroes.Commands.Update;
using Application.Features.ContentOutroes.Queries.GetById;
using Application.Features.ContentOutroes.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContentOutroes.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentOutro, CreateContentOutroCommand>().ReverseMap();
        CreateMap<ContentOutro, CreatedContentOutroResponse>().ReverseMap();
        CreateMap<ContentOutro, UpdateContentOutroCommand>().ReverseMap();
        CreateMap<ContentOutro, UpdatedContentOutroResponse>().ReverseMap();
        CreateMap<ContentOutro, DeleteContentOutroCommand>().ReverseMap();
        CreateMap<ContentOutro, DeletedContentOutroResponse>().ReverseMap();
        CreateMap<ContentOutro, GetByIdContentOutroResponse>().ReverseMap();
        CreateMap<ContentOutro, GetListContentOutroListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentOutro>, GetListResponse<GetListContentOutroListItemDto>>().ReverseMap();
    }
}