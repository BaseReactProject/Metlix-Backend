using Application.Features.Trailers.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Trailers.Constants.TrailersOperationClaims;

namespace Application.Features.Trailers.Queries.GetList;

public class GetListTrailerQuery : IRequest<GetListResponse<GetListTrailerListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListTrailers({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetTrailers";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListTrailerQueryHandler : IRequestHandler<GetListTrailerQuery, GetListResponse<GetListTrailerListItemDto>>
    {
        private readonly ITrailerRepository _trailerRepository;
        private readonly IMapper _mapper;

        public GetListTrailerQueryHandler(ITrailerRepository trailerRepository, IMapper mapper)
        {
            _trailerRepository = trailerRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTrailerListItemDto>> Handle(GetListTrailerQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Trailer> trailers = await _trailerRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTrailerListItemDto> response = _mapper.Map<GetListResponse<GetListTrailerListItemDto>>(trailers);
            return response;
        }
    }
}