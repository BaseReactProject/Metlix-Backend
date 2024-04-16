using Application.Features.ContentTrailers.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentTrailers;

public class ContentTrailersManager : IContentTrailersService
{
    private readonly IContentTrailerRepository _contentTrailerRepository;
    private readonly ContentTrailerBusinessRules _contentTrailerBusinessRules;

    public ContentTrailersManager(IContentTrailerRepository contentTrailerRepository, ContentTrailerBusinessRules contentTrailerBusinessRules)
    {
        _contentTrailerRepository = contentTrailerRepository;
        _contentTrailerBusinessRules = contentTrailerBusinessRules;
    }

    public async Task<ContentTrailer?> GetAsync(
        Expression<Func<ContentTrailer, bool>> predicate,
        Func<IQueryable<ContentTrailer>, IIncludableQueryable<ContentTrailer, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContentTrailer? contentTrailer = await _contentTrailerRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contentTrailer;
    }

    public async Task<IPaginate<ContentTrailer>?> GetListAsync(
        Expression<Func<ContentTrailer, bool>>? predicate = null,
        Func<IQueryable<ContentTrailer>, IOrderedQueryable<ContentTrailer>>? orderBy = null,
        Func<IQueryable<ContentTrailer>, IIncludableQueryable<ContentTrailer, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContentTrailer> contentTrailerList = await _contentTrailerRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentTrailerList;
    }

    public async Task<ContentTrailer> AddAsync(ContentTrailer contentTrailer)
    {
        ContentTrailer addedContentTrailer = await _contentTrailerRepository.AddAsync(contentTrailer);

        return addedContentTrailer;
    }

    public async Task<ContentTrailer> UpdateAsync(ContentTrailer contentTrailer)
    {
        ContentTrailer updatedContentTrailer = await _contentTrailerRepository.UpdateAsync(contentTrailer);

        return updatedContentTrailer;
    }

    public async Task<ContentTrailer> DeleteAsync(ContentTrailer contentTrailer, bool permanent = false)
    {
        ContentTrailer deletedContentTrailer = await _contentTrailerRepository.DeleteAsync(contentTrailer);

        return deletedContentTrailer;
    }
}
