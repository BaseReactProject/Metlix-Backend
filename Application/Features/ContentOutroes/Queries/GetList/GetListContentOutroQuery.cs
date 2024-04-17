using Application.Features.ContentOutroes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ContentOutroes.Constants.ContentOutroesOperationClaims;

namespace Application.Features.ContentOutroes.Queries.GetList;

public class GetListContentOutroQuery : IRequest<GetListResponse<GetListContentOutroListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListContentOutroes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetContentOutroes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListContentOutroQueryHandler : IRequestHandler<GetListContentOutroQuery, GetListResponse<GetListContentOutroListItemDto>>
    {
        private readonly IContentOutroRepository _contentOutroRepository;
        private readonly IMapper _mapper;

        public GetListContentOutroQueryHandler(IContentOutroRepository contentOutroRepository, IMapper mapper)
        {
            _contentOutroRepository = contentOutroRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListContentOutroListItemDto>> Handle(GetListContentOutroQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContentOutro> contentOutroes = await _contentOutroRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListContentOutroListItemDto> response = _mapper.Map<GetListResponse<GetListContentOutroListItemDto>>(contentOutroes);
            return response;
        }
    }
}