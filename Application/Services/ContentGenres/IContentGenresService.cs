using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentGenres;

public interface IContentGenresService
{
    Task<ContentGenre?> GetAsync(
        Expression<Func<ContentGenre, bool>> predicate,
        Func<IQueryable<ContentGenre>, IIncludableQueryable<ContentGenre, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContentGenre>?> GetListAsync(
        Expression<Func<ContentGenre, bool>>? predicate = null,
        Func<IQueryable<ContentGenre>, IOrderedQueryable<ContentGenre>>? orderBy = null,
        Func<IQueryable<ContentGenre>, IIncludableQueryable<ContentGenre, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContentGenre> AddAsync(ContentGenre contentGenre);
    Task<ContentGenre> UpdateAsync(ContentGenre contentGenre);
    Task<ContentGenre> DeleteAsync(ContentGenre contentGenre, bool permanent = false);
}
