using Application.Features.ContentTrailers.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentTrailers.Constants.ContentTrailersOperationClaims;

namespace Application.Features.ContentTrailers.Queries.GetList;

public class GetListContentTrailerQuery : IRequest<GetListResponse<GetListContentTrailerListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentTrailers({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentTrailers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentTrailerQueryHandler : IRequestHandler<GetListContentTrailerQuery, GetListResponse<GetListContentTrailerListItemDto>>
    {
        private readonly IContentTrailerRepository _contentTrailerRepository;
        private readonly IMapper _mapper;

        public GetListContentTrailerQueryHandler(IContentTrailerRepository contentTrailerRepository, IMapper mapper)
        {
            _contentTrailerRepository = contentTrailerRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentTrailerListItemDto>> Handle(GetListContentTrailerQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentTrailer> contentTrailers = await _contentTrailerRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentTrailerListItemDto> response = _mapper.Map<GetListResponse<GetListContentTrailerListItemDto>>(contentTrailers);
            return response;
        }
    }
}