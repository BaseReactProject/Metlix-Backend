using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentNotices;

public interface IContentNoticesService
{
    Task<ContentNotice?> GetAsync(
        Expression<Func<ContentNotice, bool>> predicate,
        Func<IQueryable<ContentNotice>, IIncludableQueryable<ContentNotice, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContentNotice>?> GetListAsync(
        Expression<Func<ContentNotice, bool>>? predicate = null,
        Func<IQueryable<ContentNotice>, IOrderedQueryable<ContentNotice>>? orderBy = null,
        Func<IQueryable<ContentNotice>, IIncludableQueryable<ContentNotice, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContentNotice> AddAsync(ContentNotice contentNotice);
    Task<ContentNotice> UpdateAsync(ContentNotice contentNotice);
    Task<ContentNotice> DeleteAsync(ContentNotice contentNotice, bool permanent = false);
}
