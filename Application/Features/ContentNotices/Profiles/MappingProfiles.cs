using Application.Features.ContentNotices.Commands.Create;
using Application.Features.ContentNotices.Commands.Delete;
using Application.Features.ContentNotices.Commands.Update;
using Application.Features.ContentNotices.Queries.GetById;
using Application.Features.ContentNotices.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.ContentNotices.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentNotice, CreateContentNoticeCommand>().ReverseMap();
        CreateMap<ContentNotice, CreatedContentNoticeResponse>().ReverseMap();
        CreateMap<ContentNotice, UpdateContentNoticeCommand>().ReverseMap();
        CreateMap<ContentNotice, UpdatedContentNoticeResponse>().ReverseMap();
        CreateMap<ContentNotice, DeleteContentNoticeCommand>().ReverseMap();
        CreateMap<ContentNotice, DeletedContentNoticeResponse>().ReverseMap();
        CreateMap<ContentNotice, GetByIdContentNoticeResponse>().ReverseMap();
        CreateMap<ContentNotice, GetListContentNoticeListItemDto>().ReverseMap();
        CreateMap<IPaginate<ContentNotice>, GetListResponse<GetListContentNoticeListItemDto>>().ReverseMap();
    }
}