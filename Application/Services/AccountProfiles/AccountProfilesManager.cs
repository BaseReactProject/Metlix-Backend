using Application.Features.AccountProfiles.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AccountProfiles;

public class AccountProfilesManager : IAccountProfilesService
{
    private readonly IAccountProfileRepository _accountProfileRepository;
    private readonly AccountProfileBusinessRules _accountProfileBusinessRules;

    public AccountProfilesManager(IAccountProfileRepository accountProfileRepository, AccountProfileBusinessRules accountProfileBusinessRules)
    {
        _accountProfileRepository = accountProfileRepository;
        _accountProfileBusinessRules = accountProfileBusinessRules;
    }

    public async Task<AccountProfile?> GetAsync(
        Expression<Func<AccountProfile, bool>> predicate,
        Func<IQueryable<AccountProfile>, IIncludableQueryable<AccountProfile, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        AccountProfile? accountProfile = await _accountProfileRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return accountProfile;
    }

    public async Task<IPaginate<AccountProfile>?> GetListAsync(
        Expression<Func<AccountProfile, bool>>? predicate = null,
        Func<IQueryable<AccountProfile>, IOrderedQueryable<AccountProfile>>? orderBy = null,
        Func<IQueryable<AccountProfile>, IIncludableQueryable<AccountProfile, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<AccountProfile> accountProfileList = await _accountProfileRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return accountProfileList;
    }

    public async Task<AccountProfile> AddAsync(AccountProfile accountProfile)
    {
        AccountProfile addedAccountProfile = await _accountProfileRepository.AddAsync(accountProfile);

        return addedAccountProfile;
    }

    public async Task<AccountProfile> UpdateAsync(AccountProfile accountProfile)
    {
        AccountProfile updatedAccountProfile = await _accountProfileRepository.UpdateAsync(accountProfile);

        return updatedAccountProfile;
    }

    public async Task<AccountProfile> DeleteAsync(AccountProfile accountProfile, bool permanent = false)
    {
        AccountProfile deletedAccountProfile = await _accountProfileRepository.DeleteAsync(accountProfile);

        return deletedAccountProfile;
    }
}
