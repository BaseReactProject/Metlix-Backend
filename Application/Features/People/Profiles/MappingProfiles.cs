using Application.Features.People.Commands.Create;
using Application.Features.People.Commands.Delete;
using Application.Features.People.Commands.Update;
using Application.Features.People.Queries.GetById;
using Application.Features.People.Queries.GetList;
using AutoMapper;
using Core.Application.Responses;
using Domain.Entities;
using Core.Persistence.Paging;

namespace Application.Features.People.Profiles;

public class MappingProfiles : AutoMapper.Profile
{
    public MappingProfiles()
    {
        CreateMap<Person, CreatePersonCommand>().ReverseMap();
        CreateMap<Person, CreatedPersonResponse>().ReverseMap();
        CreateMap<Person, UpdatePersonCommand>().ReverseMap();
        CreateMap<Person, UpdatedPersonResponse>().ReverseMap();
        CreateMap<Person, DeletePersonCommand>().ReverseMap();
        CreateMap<Person, DeletedPersonResponse>().ReverseMap();
        CreateMap<Person, GetByIdPersonResponse>().ReverseMap();
        CreateMap<Person, GetListPersonListItemDto>().ReverseMap();
        CreateMap<IPaginate<Person>, GetListResponse<GetListPersonListItemDto>>().ReverseMap();
    }
}