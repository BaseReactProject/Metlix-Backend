using Application.Features.Movies.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Movies;

public class MoviesManager : IMoviesService
{
    private readonly IMovieRepository _movieRepository;
    private readonly MovieBusinessRules _movieBusinessRules;

    public MoviesManager(IMovieRepository movieRepository, MovieBusinessRules movieBusinessRules)
    {
        _movieRepository = movieRepository;
        _movieBusinessRules = movieBusinessRules;
    }

    public async Task<Movie?> GetAsync(
        Expression<Func<Movie, bool>> predicate,
        Func<IQueryable<Movie>, IIncludableQueryable<Movie, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Movie? movie = await _movieRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return movie;
    }

    public async Task<IPaginate<Movie>?> GetListAsync(
        Expression<Func<Movie, bool>>? predicate = null,
        Func<IQueryable<Movie>, IOrderedQueryable<Movie>>? orderBy = null,
        Func<IQueryable<Movie>, IIncludableQueryable<Movie, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Movie> movieList = await _movieRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return movieList;
    }

    public async Task<Movie> AddAsync(Movie movie)
    {
        Movie addedMovie = await _movieRepository.AddAsync(movie);

        return addedMovie;
    }

    public async Task<Movie> UpdateAsync(Movie movie)
    {
        Movie updatedMovie = await _movieRepository.UpdateAsync(movie);

        return updatedMovie;
    }

    public async Task<Movie> DeleteAsync(Movie movie, bool permanent = false)
    {
        Movie deletedMovie = await _movieRepository.DeleteAsync(movie);

        return deletedMovie;
    }
}
