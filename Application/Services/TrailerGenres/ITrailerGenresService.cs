using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TrailerGenres;

public interface ITrailerGenresService
{
    Task<TrailerGenre?> GetAsync(
        Expression<Func<TrailerGenre, bool>> predicate,
        Func<IQueryable<TrailerGenre>, IIncludableQueryable<TrailerGenre, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<TrailerGenre>?> GetListAsync(
        Expression<Func<TrailerGenre, bool>>? predicate = null,
        Func<IQueryable<TrailerGenre>, IOrderedQueryable<TrailerGenre>>? orderBy = null,
        Func<IQueryable<TrailerGenre>, IIncludableQueryable<TrailerGenre, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<TrailerGenre> AddAsync(TrailerGenre trailerGenre);
    Task<TrailerGenre> UpdateAsync(TrailerGenre trailerGenre);
    Task<TrailerGenre> DeleteAsync(TrailerGenre trailerGenre, bool permanent = false);
}
