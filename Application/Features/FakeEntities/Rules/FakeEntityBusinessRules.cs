using Application.Features.FakeEntities.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.FakeEntities.Rules;

public class FakeEntityBusinessRules : BaseBusinessRules
{
    private readonly IFakeEntityRepository _fakeEntityRepository;

    public FakeEntityBusinessRules(IFakeEntityRepository fakeEntityRepository)
    {
        _fakeEntityRepository = fakeEntityRepository;
    }

    public Task FakeEntityShouldExistWhenSelected(FakeEntity? fakeEntity)
    {
        if (fakeEntity == null)
            throw new BusinessException(FakeEntitiesBusinessMessages.FakeEntityNotExists);
        return Task.CompletedTask;
    }

    public async Task FakeEntityIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        FakeEntity? fakeEntity = await _fakeEntityRepository.GetAsync(
            predicate: fe => fe.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await FakeEntityShouldExistWhenSelected(fakeEntity);
    }
}