using Application.Features.Movies.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Movies.Constants.MoviesOperationClaims;

namespace Application.Features.Movies.Queries.GetList;

public class GetListMovieQuery : IRequest<GetListResponse<GetListMovieListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListMovies({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetMovies";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListMovieQueryHandler : IRequestHandler<GetListMovieQuery, GetListResponse<GetListMovieListItemDto>>
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        public GetListMovieQueryHandler(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListMovieListItemDto>> Handle(GetListMovieQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Movie> movies = await _movieRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListMovieListItemDto> response = _mapper.Map<GetListResponse<GetListMovieListItemDto>>(movies);
            return response;
        }
    }
}