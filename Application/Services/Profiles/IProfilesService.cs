using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Profiles;

public interface IProfilesService
{
    Task<Profile?> GetAsync(
        Expression<Func<Profile, bool>> predicate,
        Func<IQueryable<Profile>, IIncludableQueryable<Profile, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Profile>?> GetListAsync(
        Expression<Func<Profile, bool>>? predicate = null,
        Func<IQueryable<Profile>, IOrderedQueryable<Profile>>? orderBy = null,
        Func<IQueryable<Profile>, IIncludableQueryable<Profile, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Profile> AddAsync(Profile profile);
    Task<Profile> UpdateAsync(Profile profile);
    Task<Profile> DeleteAsync(Profile profile, bool permanent = false);
}
