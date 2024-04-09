using Application.Features.FakeEntities.Commands.Create;
using Application.Features.FakeEntities.Commands.Delete;
using Application.Features.FakeEntities.Commands.Update;
using Application.Features.FakeEntities.Queries.GetById;
using Application.Features.FakeEntities.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.FakeEntities.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<FakeEntity, CreateFakeEntityCommand>().ReverseMap();
        CreateMap<FakeEntity, CreatedFakeEntityResponse>().ReverseMap();
        CreateMap<FakeEntity, UpdateFakeEntityCommand>().ReverseMap();
        CreateMap<FakeEntity, UpdatedFakeEntityResponse>().ReverseMap();
        CreateMap<FakeEntity, DeleteFakeEntityCommand>().ReverseMap();
        CreateMap<FakeEntity, DeletedFakeEntityResponse>().ReverseMap();
        CreateMap<FakeEntity, GetByIdFakeEntityResponse>().ReverseMap();
        CreateMap<FakeEntity, GetListFakeEntityListItemDto>().ReverseMap();
        CreateMap<IPaginate<FakeEntity>, GetListResponse<GetListFakeEntityListItemDto>>().ReverseMap();
    }
}