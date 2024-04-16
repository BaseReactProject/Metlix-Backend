using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentDirectors;

public interface IContentDirectorsService
{
    Task<ContentDirector?> GetAsync(
        Expression<Func<ContentDirector, bool>> predicate,
        Func<IQueryable<ContentDirector>, IIncludableQueryable<ContentDirector, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContentDirector>?> GetListAsync(
        Expression<Func<ContentDirector, bool>>? predicate = null,
        Func<IQueryable<ContentDirector>, IOrderedQueryable<ContentDirector>>? orderBy = null,
        Func<IQueryable<ContentDirector>, IIncludableQueryable<ContentDirector, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContentDirector> AddAsync(ContentDirector contentDirector);
    Task<ContentDirector> UpdateAsync(ContentDirector contentDirector);
    Task<ContentDirector> DeleteAsync(ContentDirector contentDirector, bool permanent = false);
}
