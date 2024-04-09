using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Qualities;

public interface IQualitiesService
{
    Task<Quality?> GetAsync(
        Expression<Func<Quality, bool>> predicate,
        Func<IQueryable<Quality>, IIncludableQueryable<Quality, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Quality>?> GetListAsync(
        Expression<Func<Quality, bool>>? predicate = null,
        Func<IQueryable<Quality>, IOrderedQueryable<Quality>>? orderBy = null,
        Func<IQueryable<Quality>, IIncludableQueryable<Quality, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Quality> AddAsync(Quality quality);
    Task<Quality> UpdateAsync(Quality quality);
    Task<Quality> DeleteAsync(Quality quality, bool permanent = false);
}
