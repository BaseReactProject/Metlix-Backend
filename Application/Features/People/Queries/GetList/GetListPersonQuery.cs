using Application.Features.People.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.People.Constants.PeopleOperationClaims;

namespace Application.Features.People.Queries.GetList;

public class GetListPersonQuery : IRequest<GetListResponse<GetListPersonListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListPeople({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetPeople";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListPersonQueryHandler : IRequestHandler<GetListPersonQuery, GetListResponse<GetListPersonListItemDto>>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public GetListPersonQueryHandler(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPersonListItemDto>> Handle(GetListPersonQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Person> people = await _personRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPersonListItemDto> response = _mapper.Map<GetListResponse<GetListPersonListItemDto>>(people);
            return response;
        }
    }
}