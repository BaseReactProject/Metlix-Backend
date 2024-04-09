using Application.Features.Qualities.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Qualities.Constants.QualitiesOperationClaims;

namespace Application.Features.Qualities.Queries.GetList;

public class GetListQualityQuery : IRequest<GetListResponse<GetListQualityListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListQualities({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetQualities";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListQualityQueryHandler : IRequestHandler<GetListQualityQuery, GetListResponse<GetListQualityListItemDto>>
    {
        private readonly IQualityRepository _qualityRepository;
        private readonly IMapper _mapper;

        public GetListQualityQueryHandler(IQualityRepository qualityRepository, IMapper mapper)
        {
            _qualityRepository = qualityRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListQualityListItemDto>> Handle(GetListQualityQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Quality> qualities = await _qualityRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListQualityListItemDto> response = _mapper.Map<GetListResponse<GetListQualityListItemDto>>(qualities);
            return response;
        }
    }
}