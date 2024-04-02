using Application.Features.FakeEntities.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.FakeEntities;

public class FakeEntitiesManager : IFakeEntitiesService
{
    private readonly IFakeEntityRepository _fakeEntityRepository;
    private readonly FakeEntityBusinessRules _fakeEntityBusinessRules;

    public FakeEntitiesManager(IFakeEntityRepository fakeEntityRepository, FakeEntityBusinessRules fakeEntityBusinessRules)
    {
        _fakeEntityRepository = fakeEntityRepository;
        _fakeEntityBusinessRules = fakeEntityBusinessRules;
    }

    public async Task<FakeEntity?> GetAsync(
        Expression<Func<FakeEntity, bool>> predicate,
        Func<IQueryable<FakeEntity>, IIncludableQueryable<FakeEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        FakeEntity? fakeEntity = await _fakeEntityRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return fakeEntity;
    }

    public async Task<IPaginate<FakeEntity>?> GetListAsync(
        Expression<Func<FakeEntity, bool>>? predicate = null,
        Func<IQueryable<FakeEntity>, IOrderedQueryable<FakeEntity>>? orderBy = null,
        Func<IQueryable<FakeEntity>, IIncludableQueryable<FakeEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<FakeEntity> fakeEntityList = await _fakeEntityRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return fakeEntityList;
    }

    public async Task<FakeEntity> AddAsync(FakeEntity fakeEntity)
    {
        FakeEntity addedFakeEntity = await _fakeEntityRepository.AddAsync(fakeEntity);

        return addedFakeEntity;
    }

    public async Task<FakeEntity> UpdateAsync(FakeEntity fakeEntity)
    {
        FakeEntity updatedFakeEntity = await _fakeEntityRepository.UpdateAsync(fakeEntity);

        return updatedFakeEntity;
    }

    public async Task<FakeEntity> DeleteAsync(FakeEntity fakeEntity, bool permanent = false)
    {
        FakeEntity deletedFakeEntity = await _fakeEntityRepository.DeleteAsync(fakeEntity);

        return deletedFakeEntity;
    }
}
