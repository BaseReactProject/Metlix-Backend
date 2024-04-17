using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentIntroes;

public interface IContentIntroesService
{
    Task<ContentIntro?> GetAsync(
        Expression<Func<ContentIntro, bool>> predicate,
        Func<IQueryable<ContentIntro>, IIncludableQueryable<ContentIntro, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContentIntro>?> GetListAsync(
        Expression<Func<ContentIntro, bool>>? predicate = null,
        Func<IQueryable<ContentIntro>, IOrderedQueryable<ContentIntro>>? orderBy = null,
        Func<IQueryable<ContentIntro>, IIncludableQueryable<ContentIntro, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContentIntro> AddAsync(ContentIntro contentIntro);
    Task<ContentIntro> UpdateAsync(ContentIntro contentIntro);
    Task<ContentIntro> DeleteAsync(ContentIntro contentIntro, bool permanent = false);
}
