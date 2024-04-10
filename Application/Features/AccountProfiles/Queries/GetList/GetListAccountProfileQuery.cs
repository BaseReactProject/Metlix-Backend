using Application.Features.AccountProfiles.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.AccountProfiles.Constants.AccountProfilesOperationClaims;

namespace Application.Features.AccountProfiles.Queries.GetList;

public class GetListAccountProfileQuery : IRequest<GetListResponse<GetListAccountProfileListItemDto>>, ISecuredRequest, ICachableRequest
{
    public GetListAccountProfileQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListAccountProfileQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListAccountProfiles({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetAccountProfiles";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAccountProfileQueryHandler : IRequestHandler<GetListAccountProfileQuery, GetListResponse<GetListAccountProfileListItemDto>>
    {
        private readonly IAccountProfileRepository _accountProfileRepository;
        private readonly IMapper _mapper;

        public GetListAccountProfileQueryHandler(IAccountProfileRepository accountProfileRepository, IMapper mapper)
        {
            _accountProfileRepository = accountProfileRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAccountProfileListItemDto>> Handle(GetListAccountProfileQuery request, CancellationToken cancellationToken)
        {
            IPaginate<AccountProfile> accountProfiles = await _accountProfileRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAccountProfileListItemDto> response = _mapper.Map<GetListResponse<GetListAccountProfileListItemDto>>(accountProfiles);
            return response;
        }
    }
}