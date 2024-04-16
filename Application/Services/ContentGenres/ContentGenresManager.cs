using Application.Features.ContentGenres.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentGenres;

public class ContentGenresManager : IContentGenresService
{
    private readonly IContentGenreRepository _contentGenreRepository;
    private readonly ContentGenreBusinessRules _contentGenreBusinessRules;

    public ContentGenresManager(IContentGenreRepository contentGenreRepository, ContentGenreBusinessRules contentGenreBusinessRules)
    {
        _contentGenreRepository = contentGenreRepository;
        _contentGenreBusinessRules = contentGenreBusinessRules;
    }

    public async Task<ContentGenre?> GetAsync(
        Expression<Func<ContentGenre, bool>> predicate,
        Func<IQueryable<ContentGenre>, IIncludableQueryable<ContentGenre, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContentGenre? contentGenre = await _contentGenreRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contentGenre;
    }

    public async Task<IPaginate<ContentGenre>?> GetListAsync(
        Expression<Func<ContentGenre, bool>>? predicate = null,
        Func<IQueryable<ContentGenre>, IOrderedQueryable<ContentGenre>>? orderBy = null,
        Func<IQueryable<ContentGenre>, IIncludableQueryable<ContentGenre, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContentGenre> contentGenreList = await _contentGenreRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentGenreList;
    }

    public async Task<ContentGenre> AddAsync(ContentGenre contentGenre)
    {
        ContentGenre addedContentGenre = await _contentGenreRepository.AddAsync(contentGenre);

        return addedContentGenre;
    }

    public async Task<ContentGenre> UpdateAsync(ContentGenre contentGenre)
    {
        ContentGenre updatedContentGenre = await _contentGenreRepository.UpdateAsync(contentGenre);

        return updatedContentGenre;
    }

    public async Task<ContentGenre> DeleteAsync(ContentGenre contentGenre, bool permanent = false)
    {
        ContentGenre deletedContentGenre = await _contentGenreRepository.DeleteAsync(contentGenre);

        return deletedContentGenre;
    }
}
