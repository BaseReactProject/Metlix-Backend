using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Movies;

public interface IMoviesService
{
    Task<Movie?> GetAsync(
        Expression<Func<Movie, bool>> predicate,
        Func<IQueryable<Movie>, IIncludableQueryable<Movie, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Movie>?> GetListAsync(
        Expression<Func<Movie, bool>>? predicate = null,
        Func<IQueryable<Movie>, IOrderedQueryable<Movie>>? orderBy = null,
        Func<IQueryable<Movie>, IIncludableQueryable<Movie, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Movie> AddAsync(Movie movie);
    Task<Movie> UpdateAsync(Movie movie);
    Task<Movie> DeleteAsync(Movie movie, bool permanent = false);
}
