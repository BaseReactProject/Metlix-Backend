using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Notices;

public interface INoticesService
{
    Task<Notice?> GetAsync(
        Expression<Func<Notice, bool>> predicate,
        Func<IQueryable<Notice>, IIncludableQueryable<Notice, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Notice>?> GetListAsync(
        Expression<Func<Notice, bool>>? predicate = null,
        Func<IQueryable<Notice>, IOrderedQueryable<Notice>>? orderBy = null,
        Func<IQueryable<Notice>, IIncludableQueryable<Notice, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Notice> AddAsync(Notice notice);
    Task<Notice> UpdateAsync(Notice notice);
    Task<Notice> DeleteAsync(Notice notice, bool permanent = false);
}
