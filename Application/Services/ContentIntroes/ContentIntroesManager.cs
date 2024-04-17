using Application.Features.ContentIntroes.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentIntroes;

public class ContentIntroesManager : IContentIntroesService
{
    private readonly IContentIntroRepository _contentIntroRepository;
    private readonly ContentIntroBusinessRules _contentIntroBusinessRules;

    public ContentIntroesManager(IContentIntroRepository contentIntroRepository, ContentIntroBusinessRules contentIntroBusinessRules)
    {
        _contentIntroRepository = contentIntroRepository;
        _contentIntroBusinessRules = contentIntroBusinessRules;
    }

    public async Task<ContentIntro?> GetAsync(
        Expression<Func<ContentIntro, bool>> predicate,
        Func<IQueryable<ContentIntro>, IIncludableQueryable<ContentIntro, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContentIntro? contentIntro = await _contentIntroRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contentIntro;
    }

    public async Task<IPaginate<ContentIntro>?> GetListAsync(
        Expression<Func<ContentIntro, bool>>? predicate = null,
        Func<IQueryable<ContentIntro>, IOrderedQueryable<ContentIntro>>? orderBy = null,
        Func<IQueryable<ContentIntro>, IIncludableQueryable<ContentIntro, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContentIntro> contentIntroList = await _contentIntroRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentIntroList;
    }

    public async Task<ContentIntro> AddAsync(ContentIntro contentIntro)
    {
        ContentIntro addedContentIntro = await _contentIntroRepository.AddAsync(contentIntro);

        return addedContentIntro;
    }

    public async Task<ContentIntro> UpdateAsync(ContentIntro contentIntro)
    {
        ContentIntro updatedContentIntro = await _contentIntroRepository.UpdateAsync(contentIntro);

        return updatedContentIntro;
    }

    public async Task<ContentIntro> DeleteAsync(ContentIntro contentIntro, bool permanent = false)
    {
        ContentIntro deletedContentIntro = await _contentIntroRepository.DeleteAsync(contentIntro);

        return deletedContentIntro;
    }
}
