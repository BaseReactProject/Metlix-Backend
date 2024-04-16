using Application.Features.ContentActors.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentActors.Constants.ContentActorsOperationClaims;

namespace Application.Features.ContentActors.Queries.GetList;

public class GetListContentActorQuery : IRequest<GetListResponse<GetListContentActorListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentActors({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentActors";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentActorQueryHandler : IRequestHandler<GetListContentActorQuery, GetListResponse<GetListContentActorListItemDto>>
    {
        private readonly IContentActorRepository _contentActorRepository;
        private readonly IMapper _mapper;

        public GetListContentActorQueryHandler(IContentActorRepository contentActorRepository, IMapper mapper)
        {
            _contentActorRepository = contentActorRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentActorListItemDto>> Handle(GetListContentActorQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentActor> contentActors = await _contentActorRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentActorListItemDto> response = _mapper.Map<GetListResponse<GetListContentActorListItemDto>>(contentActors);
            return response;
        }
    }
}