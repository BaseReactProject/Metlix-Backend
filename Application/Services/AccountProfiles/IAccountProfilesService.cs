using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AccountProfiles;

public interface IAccountProfilesService
{
    Task<AccountProfile?> GetAsync(
        Expression<Func<AccountProfile, bool>> predicate,
        Func<IQueryable<AccountProfile>, IIncludableQueryable<AccountProfile, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<AccountProfile>?> GetListAsync(
        Expression<Func<AccountProfile, bool>>? predicate = null,
        Func<IQueryable<AccountProfile>, IOrderedQueryable<AccountProfile>>? orderBy = null,
        Func<IQueryable<AccountProfile>, IIncludableQueryable<AccountProfile, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<AccountProfile> AddAsync(AccountProfile accountProfile);
    Task<AccountProfile> UpdateAsync(AccountProfile accountProfile);
    Task<AccountProfile> DeleteAsync(AccountProfile accountProfile, bool permanent = false);
}
