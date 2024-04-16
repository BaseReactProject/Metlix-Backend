using Application.Features.ContentNotices.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentNotices.Constants.ContentNoticesOperationClaims;

namespace Application.Features.ContentNotices.Queries.GetList;

public class GetListContentNoticeQuery : IRequest<GetListResponse<GetListContentNoticeListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentNotices({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentNotices";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentNoticeQueryHandler : IRequestHandler<GetListContentNoticeQuery, GetListResponse<GetListContentNoticeListItemDto>>
    {
        private readonly IContentNoticeRepository _contentNoticeRepository;
        private readonly IMapper _mapper;

        public GetListContentNoticeQueryHandler(IContentNoticeRepository contentNoticeRepository, IMapper mapper)
        {
            _contentNoticeRepository = contentNoticeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentNoticeListItemDto>> Handle(GetListContentNoticeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentNotice> contentNotices = await _contentNoticeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentNoticeListItemDto> response = _mapper.Map<GetListResponse<GetListContentNoticeListItemDto>>(contentNotices);
            return response;
        }
    }
}