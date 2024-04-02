using Application.Features.FakeEntities.Constants;
using Application.Features.FakeEntities.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.FakeEntities.Constants.FakeEntitiesOperationClaims;

namespace Application.Features.FakeEntities.Commands.Create;

public class CreateFakeEntityCommand : IRequest<CreatedFakeEntityResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public int BrandId { get; set; }

    public string[] Roles => new[] { Admin, Write, FakeEntitiesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetFakeEntities";

    public class CreateFakeEntityCommandHandler : IRequestHandler<CreateFakeEntityCommand, CreatedFakeEntityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFakeEntityRepository _fakeEntityRepository;
        private readonly FakeEntityBusinessRules _fakeEntityBusinessRules;

        public CreateFakeEntityCommandHandler(IMapper mapper, IFakeEntityRepository fakeEntityRepository,
                                         FakeEntityBusinessRules fakeEntityBusinessRules)
        {
            _mapper = mapper;
            _fakeEntityRepository = fakeEntityRepository;
            _fakeEntityBusinessRules = fakeEntityBusinessRules;
        }

        public async Task<CreatedFakeEntityResponse> Handle(CreateFakeEntityCommand request, CancellationToken cancellationToken)
        {
            FakeEntity fakeEntity = _mapper.Map<FakeEntity>(request);

            await _fakeEntityRepository.AddAsync(fakeEntity);

            CreatedFakeEntityResponse response = _mapper.Map<CreatedFakeEntityResponse>(fakeEntity);
            return response;
        }
    }
}