using Application.Features.ContentGenres.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentGenres.Constants.ContentGenresOperationClaims;

namespace Application.Features.ContentGenres.Queries.GetList;

public class GetListContentGenreQuery : IRequest<GetListResponse<GetListContentGenreListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentGenres({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentGenres";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentGenreQueryHandler : IRequestHandler<GetListContentGenreQuery, GetListResponse<GetListContentGenreListItemDto>>
    {
        private readonly IContentGenreRepository _contentGenreRepository;
        private readonly IMapper _mapper;

        public GetListContentGenreQueryHandler(IContentGenreRepository contentGenreRepository, IMapper mapper)
        {
            _contentGenreRepository = contentGenreRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentGenreListItemDto>> Handle(GetListContentGenreQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentGenre> contentGenres = await _contentGenreRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentGenreListItemDto> response = _mapper.Map<GetListResponse<GetListContentGenreListItemDto>>(contentGenres);
            return response;
        }
    }
}