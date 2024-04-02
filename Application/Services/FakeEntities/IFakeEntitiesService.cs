using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.FakeEntities;

public interface IFakeEntitiesService
{
    Task<FakeEntity?> GetAsync(
        Expression<Func<FakeEntity, bool>> predicate,
        Func<IQueryable<FakeEntity>, IIncludableQueryable<FakeEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<FakeEntity>?> GetListAsync(
        Expression<Func<FakeEntity, bool>>? predicate = null,
        Func<IQueryable<FakeEntity>, IOrderedQueryable<FakeEntity>>? orderBy = null,
        Func<IQueryable<FakeEntity>, IIncludableQueryable<FakeEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<FakeEntity> AddAsync(FakeEntity fakeEntity);
    Task<FakeEntity> UpdateAsync(FakeEntity fakeEntity);
    Task<FakeEntity> DeleteAsync(FakeEntity fakeEntity, bool permanent = false);
}
