using Application.Features.Genres.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Genres.Constants.GenresOperationClaims;

namespace Application.Features.Genres.Queries.GetList;

public class GetListGenreQuery : IRequest<GetListResponse<GetListGenreListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListGenres({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetGenres";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListGenreQueryHandler : IRequestHandler<GetListGenreQuery, GetListResponse<GetListGenreListItemDto>>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GetListGenreQueryHandler(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListGenreListItemDto>> Handle(GetListGenreQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Genre> genres = await _genreRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListGenreListItemDto> response = _mapper.Map<GetListResponse<GetListGenreListItemDto>>(genres);
            return response;
        }
    }
}