using Application.Features.FakeEntities.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.FakeEntities.Constants.FakeEntitiesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FakeEntities.Queries.GetList;

public class GetListFakeEntityQuery : IRequest<GetListResponse<GetListFakeEntityListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListFakeEntities({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetFakeEntities";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListFakeEntityQueryHandler : IRequestHandler<GetListFakeEntityQuery, GetListResponse<GetListFakeEntityListItemDto>>
    {
        private readonly IFakeEntityRepository _fakeEntityRepository;
        private readonly IMapper _mapper;

        public GetListFakeEntityQueryHandler(IFakeEntityRepository fakeEntityRepository, IMapper mapper)
        {
            _fakeEntityRepository = fakeEntityRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListFakeEntityListItemDto>> Handle(GetListFakeEntityQuery request, CancellationToken cancellationToken)
        {
            IPaginate<FakeEntity> fakeEntities = await _fakeEntityRepository.GetListAsync(
                predicate: fe => fe.Id == 2,
                include: fe => fe.Include(fe => fe.Brand),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            ); ;

            GetListResponse<GetListFakeEntityListItemDto> response = _mapper.Map<GetListResponse<GetListFakeEntityListItemDto>>(fakeEntities);
            return response;
        }
    }
}