using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentActors;

public interface IContentActorsService
{
    Task<ContentActor?> GetAsync(
        Expression<Func<ContentActor, bool>> predicate,
        Func<IQueryable<ContentActor>, IIncludableQueryable<ContentActor, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ContentActor>?> GetListAsync(
        Expression<Func<ContentActor, bool>>? predicate = null,
        Func<IQueryable<ContentActor>, IOrderedQueryable<ContentActor>>? orderBy = null,
        Func<IQueryable<ContentActor>, IIncludableQueryable<ContentActor, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ContentActor> AddAsync(ContentActor contentActor);
    Task<ContentActor> UpdateAsync(ContentActor contentActor);
    Task<ContentActor> DeleteAsync(ContentActor contentActor, bool permanent = false);
}
