using Application.Features.Notices.Commands.Create;
using Application.Features.Notices.Commands.Delete;
using Application.Features.Notices.Commands.Update;
using Application.Features.Notices.Queries.GetById;
using Application.Features.Notices.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Notices.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<Notice, CreateNoticeCommand>().ReverseMap();
        CreateMap<Notice, CreatedNoticeResponse>().ReverseMap();
        CreateMap<Notice, UpdateNoticeCommand>().ReverseMap();
        CreateMap<Notice, UpdatedNoticeResponse>().ReverseMap();
        CreateMap<Notice, DeleteNoticeCommand>().ReverseMap();
        CreateMap<Notice, DeletedNoticeResponse>().ReverseMap();
        CreateMap<Notice, GetByIdNoticeResponse>().ReverseMap();
        CreateMap<Notice, GetListNoticeListItemDto>().ReverseMap();
        CreateMap<IPaginate<Notice>, GetListResponse<GetListNoticeListItemDto>>().ReverseMap();
    }
}