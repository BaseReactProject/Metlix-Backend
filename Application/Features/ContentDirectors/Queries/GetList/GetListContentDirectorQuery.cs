using Application.Features.ContentDirectors.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentDirectors.Constants.ContentDirectorsOperationClaims;

namespace Application.Features.ContentDirectors.Queries.GetList;

public class GetListContentDirectorQuery : IRequest<GetListResponse<GetListContentDirectorListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentDirectors({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentDirectors";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentDirectorQueryHandler : IRequestHandler<GetListContentDirectorQuery, GetListResponse<GetListContentDirectorListItemDto>>
    {
        private readonly IContentDirectorRepository _contentDirectorRepository;
        private readonly IMapper _mapper;

        public GetListContentDirectorQueryHandler(IContentDirectorRepository contentDirectorRepository, IMapper mapper)
        {
            _contentDirectorRepository = contentDirectorRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentDirectorListItemDto>> Handle(GetListContentDirectorQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentDirector> contentDirectors = await _contentDirectorRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentDirectorListItemDto> response = _mapper.Map<GetListResponse<GetListContentDirectorListItemDto>>(contentDirectors);
            return response;
        }
    }
}