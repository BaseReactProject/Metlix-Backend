using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Trailers;

public interface ITrailersService
{
    Task<Trailer?> GetAsync(
        Expression<Func<Trailer, bool>> predicate,
        Func<IQueryable<Trailer>, IIncludableQueryable<Trailer, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Trailer>?> GetListAsync(
        Expression<Func<Trailer, bool>>? predicate = null,
        Func<IQueryable<Trailer>, IOrderedQueryable<Trailer>>? orderBy = null,
        Func<IQueryable<Trailer>, IIncludableQueryable<Trailer, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Trailer> AddAsync(Trailer trailer);
    Task<Trailer> UpdateAsync(Trailer trailer);
    Task<Trailer> DeleteAsync(Trailer trailer, bool permanent = false);
}
