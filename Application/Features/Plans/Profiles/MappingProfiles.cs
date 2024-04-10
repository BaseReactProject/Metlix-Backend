using Application.Features.Plans.Commands.Create;
using Application.Features.Plans.Commands.Delete;
using Application.Features.Plans.Commands.Update;
using Application.Features.Plans.Queries.GetById;
using Application.Features.Plans.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.Plans.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<Plan, CreatePlanCommand>().ReverseMap();
        CreateMap<Plan, CreatedPlanResponse>().ReverseMap();
        CreateMap<Plan, UpdatePlanCommand>().ReverseMap();
        CreateMap<Plan, UpdatedPlanResponse>().ReverseMap();
        CreateMap<Plan, DeletePlanCommand>().ReverseMap();
        CreateMap<Plan, DeletedPlanResponse>().ReverseMap();
        CreateMap<Plan, GetByIdPlanResponse>().ReverseMap();
        CreateMap<Plan, GetListPlanListItemDto>().ReverseMap();
        CreateMap<IPaginate<Plan>, GetListResponse<GetListPlanListItemDto>>().ReverseMap();
    }
}