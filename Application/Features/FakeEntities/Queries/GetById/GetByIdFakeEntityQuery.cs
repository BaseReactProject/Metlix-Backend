using Application.Features.FakeEntities.Constants;
using Application.Features.FakeEntities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.FakeEntities.Constants.FakeEntitiesOperationClaims;

namespace Application.Features.FakeEntities.Queries.GetById;

public class GetByIdFakeEntityQuery : IRequest<GetByIdFakeEntityResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdFakeEntityQueryHandler : IRequestHandler<GetByIdFakeEntityQuery, GetByIdFakeEntityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFakeEntityRepository _fakeEntityRepository;
        private readonly FakeEntityBusinessRules _fakeEntityBusinessRules;

        public GetByIdFakeEntityQueryHandler(IMapper mapper, IFakeEntityRepository fakeEntityRepository, FakeEntityBusinessRules fakeEntityBusinessRules)
        {
            _mapper = mapper;
            _fakeEntityRepository = fakeEntityRepository;
            _fakeEntityBusinessRules = fakeEntityBusinessRules;
        }

        public async Task<GetByIdFakeEntityResponse> Handle(GetByIdFakeEntityQuery request, CancellationToken cancellationToken)
        {
            FakeEntity? fakeEntity = await _fakeEntityRepository.GetAsync(predicate: fe => fe.Id == request.Id, cancellationToken: cancellationToken);
            await _fakeEntityBusinessRules.FakeEntityShouldExistWhenSelected(fakeEntity);

            GetByIdFakeEntityResponse response = _mapper.Map<GetByIdFakeEntityResponse>(fakeEntity);
            return response;
        }
    }
}