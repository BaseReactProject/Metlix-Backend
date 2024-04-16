using Application.Features.ContentScenarists.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentScenarists;

public class ContentScenaristsManager : IContentScenaristsService
{
    private readonly IContentScenaristRepository _contentScenaristRepository;
    private readonly ContentScenaristBusinessRules _contentScenaristBusinessRules;

    public ContentScenaristsManager(IContentScenaristRepository contentScenaristRepository, ContentScenaristBusinessRules contentScenaristBusinessRules)
    {
        _contentScenaristRepository = contentScenaristRepository;
        _contentScenaristBusinessRules = contentScenaristBusinessRules;
    }

    public async Task<ContentScenarist?> GetAsync(
        Expression<Func<ContentScenarist, bool>> predicate,
        Func<IQueryable<ContentScenarist>, IIncludableQueryable<ContentScenarist, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContentScenarist? contentScenarist = await _contentScenaristRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contentScenarist;
    }

    public async Task<IPaginate<ContentScenarist>?> GetListAsync(
        Expression<Func<ContentScenarist, bool>>? predicate = null,
        Func<IQueryable<ContentScenarist>, IOrderedQueryable<ContentScenarist>>? orderBy = null,
        Func<IQueryable<ContentScenarist>, IIncludableQueryable<ContentScenarist, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContentScenarist> contentScenaristList = await _contentScenaristRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentScenaristList;
    }

    public async Task<ContentScenarist> AddAsync(ContentScenarist contentScenarist)
    {
        ContentScenarist addedContentScenarist = await _contentScenaristRepository.AddAsync(contentScenarist);

        return addedContentScenarist;
    }

    public async Task<ContentScenarist> UpdateAsync(ContentScenarist contentScenarist)
    {
        ContentScenarist updatedContentScenarist = await _contentScenaristRepository.UpdateAsync(contentScenarist);

        return updatedContentScenarist;
    }

    public async Task<ContentScenarist> DeleteAsync(ContentScenarist contentScenarist, bool permanent = false)
    {
        ContentScenarist deletedContentScenarist = await _contentScenaristRepository.DeleteAsync(contentScenarist);

        return deletedContentScenarist;
    }
}
