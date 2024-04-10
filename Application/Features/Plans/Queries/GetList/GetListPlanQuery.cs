using Application.Features.Plans.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Plans.Constants.PlansOperationClaims;

namespace Application.Features.Plans.Queries.GetList;

public class GetListPlanQuery : IRequest<GetListResponse<GetListPlanListItemDto>>, ISecuredRequest, ICachableRequest
{
    public GetListPlanQuery()
    {
        PageRequest = new PageRequest { PageIndex = 0, PageSize = 10 };
    }

    public GetListPlanQuery(PageRequest pageRequest)
    {
        PageRequest = pageRequest;
    }

    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListPlans({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetPlans";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListPlanQueryHandler : IRequestHandler<GetListPlanQuery, GetListResponse<GetListPlanListItemDto>>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;

        public GetListPlanQueryHandler(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListPlanListItemDto>> Handle(GetListPlanQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Plan> plans = await _planRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListPlanListItemDto> response = _mapper.Map<GetListResponse<GetListPlanListItemDto>>(plans);
            return response;
        }
    }
}