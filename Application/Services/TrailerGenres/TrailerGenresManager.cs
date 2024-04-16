using Application.Features.TrailerGenres.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.TrailerGenres;

public class TrailerGenresManager : ITrailerGenresService
{
    private readonly ITrailerGenreRepository _trailerGenreRepository;
    private readonly TrailerGenreBusinessRules _trailerGenreBusinessRules;

    public TrailerGenresManager(ITrailerGenreRepository trailerGenreRepository, TrailerGenreBusinessRules trailerGenreBusinessRules)
    {
        _trailerGenreRepository = trailerGenreRepository;
        _trailerGenreBusinessRules = trailerGenreBusinessRules;
    }

    public async Task<TrailerGenre?> GetAsync(
        Expression<Func<TrailerGenre, bool>> predicate,
        Func<IQueryable<TrailerGenre>, IIncludableQueryable<TrailerGenre, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        TrailerGenre? trailerGenre = await _trailerGenreRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return trailerGenre;
    }

    public async Task<IPaginate<TrailerGenre>?> GetListAsync(
        Expression<Func<TrailerGenre, bool>>? predicate = null,
        Func<IQueryable<TrailerGenre>, IOrderedQueryable<TrailerGenre>>? orderBy = null,
        Func<IQueryable<TrailerGenre>, IIncludableQueryable<TrailerGenre, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<TrailerGenre> trailerGenreList = await _trailerGenreRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return trailerGenreList;
    }

    public async Task<TrailerGenre> AddAsync(TrailerGenre trailerGenre)
    {
        TrailerGenre addedTrailerGenre = await _trailerGenreRepository.AddAsync(trailerGenre);

        return addedTrailerGenre;
    }

    public async Task<TrailerGenre> UpdateAsync(TrailerGenre trailerGenre)
    {
        TrailerGenre updatedTrailerGenre = await _trailerGenreRepository.UpdateAsync(trailerGenre);

        return updatedTrailerGenre;
    }

    public async Task<TrailerGenre> DeleteAsync(TrailerGenre trailerGenre, bool permanent = false)
    {
        TrailerGenre deletedTrailerGenre = await _trailerGenreRepository.DeleteAsync(trailerGenre);

        return deletedTrailerGenre;
    }
}
