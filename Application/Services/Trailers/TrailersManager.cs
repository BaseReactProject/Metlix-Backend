using Application.Features.Trailers.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Trailers;

public class TrailersManager : ITrailersService
{
    private readonly ITrailerRepository _trailerRepository;
    private readonly TrailerBusinessRules _trailerBusinessRules;

    public TrailersManager(ITrailerRepository trailerRepository, TrailerBusinessRules trailerBusinessRules)
    {
        _trailerRepository = trailerRepository;
        _trailerBusinessRules = trailerBusinessRules;
    }

    public async Task<Trailer?> GetAsync(
        Expression<Func<Trailer, bool>> predicate,
        Func<IQueryable<Trailer>, IIncludableQueryable<Trailer, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Trailer? trailer = await _trailerRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return trailer;
    }

    public async Task<IPaginate<Trailer>?> GetListAsync(
        Expression<Func<Trailer, bool>>? predicate = null,
        Func<IQueryable<Trailer>, IOrderedQueryable<Trailer>>? orderBy = null,
        Func<IQueryable<Trailer>, IIncludableQueryable<Trailer, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Trailer> trailerList = await _trailerRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return trailerList;
    }

    public async Task<Trailer> AddAsync(Trailer trailer)
    {
        Trailer addedTrailer = await _trailerRepository.AddAsync(trailer);

        return addedTrailer;
    }

    public async Task<Trailer> UpdateAsync(Trailer trailer)
    {
        Trailer updatedTrailer = await _trailerRepository.UpdateAsync(trailer);

        return updatedTrailer;
    }

    public async Task<Trailer> DeleteAsync(Trailer trailer, bool permanent = false)
    {
        Trailer deletedTrailer = await _trailerRepository.DeleteAsync(trailer);

        return deletedTrailer;
    }
}
