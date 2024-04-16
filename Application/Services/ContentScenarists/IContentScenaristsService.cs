using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentScenarists;

public interface IContentScenaristsService
{
    Task<ContentScenarist?> GetAsync(
        Expression<Func<ContentScenarist, bool>> predicate,
        Func<IQueryable<ContentScenarist>, IIncludableQueryable<ContentScenarist, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContentScenarist>?> GetListAsync(
        Expression<Func<ContentScenarist, bool>>? predicate = null,
        Func<IQueryable<ContentScenarist>, IOrderedQueryable<ContentScenarist>>? orderBy = null,
        Func<IQueryable<ContentScenarist>, IIncludableQueryable<ContentScenarist, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContentScenarist> AddAsync(ContentScenarist contentScenarist);
    Task<ContentScenarist> UpdateAsync(ContentScenarist contentScenarist);
    Task<ContentScenarist> DeleteAsync(ContentScenarist contentScenarist, bool permanent = false);
}
