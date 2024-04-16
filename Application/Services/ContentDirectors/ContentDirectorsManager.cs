using Application.Features.ContentDirectors.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentDirectors;

public class ContentDirectorsManager : IContentDirectorsService
{
    private readonly IContentDirectorRepository _contentDirectorRepository;
    private readonly ContentDirectorBusinessRules _contentDirectorBusinessRules;

    public ContentDirectorsManager(IContentDirectorRepository contentDirectorRepository, ContentDirectorBusinessRules contentDirectorBusinessRules)
    {
        _contentDirectorRepository = contentDirectorRepository;
        _contentDirectorBusinessRules = contentDirectorBusinessRules;
    }

    public async Task<ContentDirector?> GetAsync(
        Expression<Func<ContentDirector, bool>> predicate,
        Func<IQueryable<ContentDirector>, IIncludableQueryable<ContentDirector, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContentDirector? contentDirector = await _contentDirectorRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contentDirector;
    }

    public async Task<IPaginate<ContentDirector>?> GetListAsync(
        Expression<Func<ContentDirector, bool>>? predicate = null,
        Func<IQueryable<ContentDirector>, IOrderedQueryable<ContentDirector>>? orderBy = null,
        Func<IQueryable<ContentDirector>, IIncludableQueryable<ContentDirector, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContentDirector> contentDirectorList = await _contentDirectorRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentDirectorList;
    }

    public async Task<ContentDirector> AddAsync(ContentDirector contentDirector)
    {
        ContentDirector addedContentDirector = await _contentDirectorRepository.AddAsync(contentDirector);

        return addedContentDirector;
    }

    public async Task<ContentDirector> UpdateAsync(ContentDirector contentDirector)
    {
        ContentDirector updatedContentDirector = await _contentDirectorRepository.UpdateAsync(contentDirector);

        return updatedContentDirector;
    }

    public async Task<ContentDirector> DeleteAsync(ContentDirector contentDirector, bool permanent = false)
    {
        ContentDirector deletedContentDirector = await _contentDirectorRepository.DeleteAsync(contentDirector);

        return deletedContentDirector;
    }
}
