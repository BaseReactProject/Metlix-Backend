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

namespace Application.Features.FakeEntities.Commands.Update;

public class UpdateFakeEntityCommand : IRequest<UpdatedFakeEntityResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BrandId { get; set; }

    public string[] Roles => new[] { Admin, Write, FakeEntitiesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetFakeEntities";

    public class UpdateFakeEntityCommandHandler : IRequestHandler<UpdateFakeEntityCommand, UpdatedFakeEntityResponse>
    {
        private readonly IMapper _mapper;
        private readonly IFakeEntityRepository _fakeEntityRepository;
        private readonly FakeEntityBusinessRules _fakeEntityBusinessRules;

        public UpdateFakeEntityCommandHandler(IMapper mapper, IFakeEntityRepository fakeEntityRepository,
                                         FakeEntityBusinessRules fakeEntityBusinessRules)
        {
            _mapper = mapper;
            _fakeEntityRepository = fakeEntityRepository;
            _fakeEntityBusinessRules = fakeEntityBusinessRules;
        }

        public async Task<UpdatedFakeEntityResponse> Handle(UpdateFakeEntityCommand request, CancellationToken cancellationToken)
        {
            FakeEntity? fakeEntity = await _fakeEntityRepository.GetAsync(predicate: fe => fe.Id == request.Id, cancellationToken: cancellationToken);
            await _fakeEntityBusinessRules.FakeEntityShouldExistWhenSelected(fakeEntity);
            fakeEntity = _mapper.Map(request, fakeEntity);

            await _fakeEntityRepository.UpdateAsync(fakeEntity!);

            UpdatedFakeEntityResponse response = _mapper.Map<UpdatedFakeEntityResponse>(fakeEntity);
            return response;
        }
    }
}