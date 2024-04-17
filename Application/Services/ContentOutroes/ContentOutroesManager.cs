using Application.Features.ContentOutroes.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentOutroes;

public class ContentOutroesManager : IContentOutroesService
{
    private readonly IContentOutroRepository _contentOutroRepository;
    private readonly ContentOutroBusinessRules _contentOutroBusinessRules;

    public ContentOutroesManager(IContentOutroRepository contentOutroRepository, ContentOutroBusinessRules contentOutroBusinessRules)
    {
        _contentOutroRepository = contentOutroRepository;
        _contentOutroBusinessRules = contentOutroBusinessRules;
    }

    public async Task<ContentOutro?> GetAsync(
        Expression<Func<ContentOutro, bool>> predicate,
        Func<IQueryable<ContentOutro>, IIncludableQueryable<ContentOutro, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContentOutro? contentOutro = await _contentOutroRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contentOutro;
    }

    public async Task<IPaginate<ContentOutro>?> GetListAsync(
        Expression<Func<ContentOutro, bool>>? predicate = null,
        Func<IQueryable<ContentOutro>, IOrderedQueryable<ContentOutro>>? orderBy = null,
        Func<IQueryable<ContentOutro>, IIncludableQueryable<ContentOutro, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContentOutro> contentOutroList = await _contentOutroRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentOutroList;
    }

    public async Task<ContentOutro> AddAsync(ContentOutro contentOutro)
    {
        ContentOutro addedContentOutro = await _contentOutroRepository.AddAsync(contentOutro);

        return addedContentOutro;
    }

    public async Task<ContentOutro> UpdateAsync(ContentOutro contentOutro)
    {
        ContentOutro updatedContentOutro = await _contentOutroRepository.UpdateAsync(contentOutro);

        return updatedContentOutro;
    }

    public async Task<ContentOutro> DeleteAsync(ContentOutro contentOutro, bool permanent = false)
    {
        ContentOutro deletedContentOutro = await _contentOutroRepository.DeleteAsync(contentOutro);

        return deletedContentOutro;
    }
}
