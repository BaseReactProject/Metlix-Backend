using Application.Features.ContentScenarists.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentScenarists.Constants.ContentScenaristsOperationClaims;

namespace Application.Features.ContentScenarists.Queries.GetList;

public class GetListContentScenaristQuery : IRequest<GetListResponse<GetListContentScenaristListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentScenarists({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentScenarists";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentScenaristQueryHandler : IRequestHandler<GetListContentScenaristQuery, GetListResponse<GetListContentScenaristListItemDto>>
    {
        private readonly IContentScenaristRepository _contentScenaristRepository;
        private readonly IMapper _mapper;

        public GetListContentScenaristQueryHandler(IContentScenaristRepository contentScenaristRepository, IMapper mapper)
        {
            _contentScenaristRepository = contentScenaristRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentScenaristListItemDto>> Handle(GetListContentScenaristQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentScenarist> contentScenarists = await _contentScenaristRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentScenaristListItemDto> response = _mapper.Map<GetListResponse<GetListContentScenaristListItemDto>>(contentScenarists);
            return response;
        }
    }
}