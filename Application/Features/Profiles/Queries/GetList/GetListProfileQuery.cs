using Application.Features.Profiles.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Profiles.Constants.ProfilesOperationClaims;

namespace Application.Features.Profiles.Queries.GetList;

public class GetListProfileQuery : IRequest<GetListResponse<GetListProfileListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListProfiles({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetProfiles";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListProfileQueryHandler : IRequestHandler<GetListProfileQuery, GetListResponse<GetListProfileListItemDto>>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMapper _mapper;

        public GetListProfileQueryHandler(IProfileRepository profileRepository, IMapper mapper)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProfileListItemDto>> Handle(GetListProfileQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Profile> profiles = await _profileRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListProfileListItemDto> response = _mapper.Map<GetListResponse<GetListProfileListItemDto>>(profiles);
            return response;
        }
    }
}