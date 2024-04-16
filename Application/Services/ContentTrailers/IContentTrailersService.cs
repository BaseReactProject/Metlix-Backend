using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentTrailers;

public interface IContentTrailersService
{
    Task<ContentTrailer?> GetAsync(
        Expression<Func<ContentTrailer, bool>> predicate,
        Func<IQueryable<ContentTrailer>, IIncludableQueryable<ContentTrailer, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContentTrailer>?> GetListAsync(
        Expression<Func<ContentTrailer, bool>>? predicate = null,
        Func<IQueryable<ContentTrailer>, IOrderedQueryable<ContentTrailer>>? orderBy = null,
        Func<IQueryable<ContentTrailer>, IIncludableQueryable<ContentTrailer, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContentTrailer> AddAsync(ContentTrailer contentTrailer);
    Task<ContentTrailer> UpdateAsync(ContentTrailer contentTrailer);
    Task<ContentTrailer> DeleteAsync(ContentTrailer contentTrailer, bool permanent = false);
}
