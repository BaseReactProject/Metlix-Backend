using Application.Features.Notices.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Notices.Constants.NoticesOperationClaims;

namespace Application.Features.Notices.Queries.GetList;

public class GetListNoticeQuery : IRequest<GetListResponse<GetListNoticeListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListNotices({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetNotices";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListNoticeQueryHandler : IRequestHandler<GetListNoticeQuery, GetListResponse<GetListNoticeListItemDto>>
    {
        private readonly INoticeRepository _noticeRepository;
        private readonly IMapper _mapper;

        public GetListNoticeQueryHandler(INoticeRepository noticeRepository, IMapper mapper)
        {
            _noticeRepository = noticeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListNoticeListItemDto>> Handle(GetListNoticeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Notice> notices = await _noticeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListNoticeListItemDto> response = _mapper.Map<GetListResponse<GetListNoticeListItemDto>>(notices);
            return response;
        }
    }
}