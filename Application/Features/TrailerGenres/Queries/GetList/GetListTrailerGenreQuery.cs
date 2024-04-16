using Application.Features.TrailerGenres.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.TrailerGenres.Constants.TrailerGenresOperationClaims;

namespace Application.Features.TrailerGenres.Queries.GetList;

public class GetListTrailerGenreQuery : IRequest<GetListResponse<GetListTrailerGenreListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListTrailerGenres({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetTrailerGenres";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListTrailerGenreQueryHandler : IRequestHandler<GetListTrailerGenreQuery, GetListResponse<GetListTrailerGenreListItemDto>>
    {
        private readonly ITrailerGenreRepository _trailerGenreRepository;
        private readonly IMapper _mapper;

        public GetListTrailerGenreQueryHandler(ITrailerGenreRepository trailerGenreRepository, IMapper mapper)
        {
            _trailerGenreRepository = trailerGenreRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTrailerGenreListItemDto>> Handle(GetListTrailerGenreQuery request, CancellationToken cancellationToken)
        {
            IPaginate<TrailerGenre> trailerGenres = await _trailerGenreRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTrailerGenreListItemDto> response = _mapper.Map<GetListResponse<GetListTrailerGenreListItemDto>>(trailerGenres);
            return response;
        }
    }
}