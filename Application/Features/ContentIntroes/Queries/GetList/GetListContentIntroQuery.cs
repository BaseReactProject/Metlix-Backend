using Application.Features.ContentIntroes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentIntroes.Constants.ContentIntroesOperationClaims;

namespace Application.Features.ContentIntroes.Queries.GetList;

public class GetListContentIntroQuery : IRequest<GetListResponse<GetListContentIntroListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentIntroes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentIntroes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentIntroQueryHandler : IRequestHandler<GetListContentIntroQuery, GetListResponse<GetListContentIntroListItemDto>>
    {
        private readonly IContentIntroRepository _contentIntroRepository;
        private readonly IMapper _mapper;

        public GetListContentIntroQueryHandler(IContentIntroRepository contentIntroRepository, IMapper mapper)
        {
            _contentIntroRepository = contentIntroRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentIntroListItemDto>> Handle(GetListContentIntroQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentIntro> contentIntroes = await _contentIntroRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentIntroListItemDto> response = _mapper.Map<GetListResponse<GetListContentIntroListItemDto>>(contentIntroes);
            return response;
        }
    }
}