using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentOutroes;

public interface IContentOutroesService
{
    Task<ContentOutro?> GetAsync(
        Expression<Func<ContentOutro, bool>> predicate,
        Func<IQueryable<ContentOutro>, IIncludableQueryable<ContentOutro, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContentOutro>?> GetListAsync(
        Expression<Func<ContentOutro, bool>>? predicate = null,
        Func<IQueryable<ContentOutro>, IOrderedQueryable<ContentOutro>>? orderBy = null,
        Func<IQueryable<ContentOutro>, IIncludableQueryable<ContentOutro, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContentOutro> AddAsync(ContentOutro contentOutro);
    Task<ContentOutro> UpdateAsync(ContentOutro contentOutro);
    Task<ContentOutro> DeleteAsync(ContentOutro contentOutro, bool permanent = false);
}
