using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Plans;

public interface IPlansService
{
    Task<Plan?> GetAsync(
        Expression<Func<Plan, bool>> predicate,
        Func<IQueryable<Plan>, IIncludableQueryable<Plan, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Plan>?> GetListAsync(
        Expression<Func<Plan, bool>>? predicate = null,
        Func<IQueryable<Plan>, IOrderedQueryable<Plan>>? orderBy = null,
        Func<IQueryable<Plan>, IIncludableQueryable<Plan, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Plan> AddAsync(Plan plan);
    Task<Plan> UpdateAsync(Plan plan);
    Task<Plan> DeleteAsync(Plan plan, bool permanent = false);
}
