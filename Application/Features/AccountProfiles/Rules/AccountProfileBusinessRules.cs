using Application.Features.AccountProfiles.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.AccountProfiles.Rules;

public class AccountProfileBusinessRules : BaseBusinessRules
{
    private readonly IAccountProfileRepository _accountProfileRepository;

    public AccountProfileBusinessRules(IAccountProfileRepository accountProfileRepository)
    {
        _accountProfileRepository = accountProfileRepository;
    }

    public Task AccountProfileShouldExistWhenSelected(AccountProfile? accountProfile)
    {
        if (accountProfile == null)
            throw new BusinessException(AccountProfilesBusinessMessages.AccountProfileNotExists);
        return Task.CompletedTask;
    }

    public async Task AccountProfileIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        AccountProfile? accountProfile = await _accountProfileRepository.GetAsync(
            predicate: ap => ap.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AccountProfileShouldExistWhenSelected(accountProfile);
    }
}