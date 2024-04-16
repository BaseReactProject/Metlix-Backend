using Application.Features.Profiles.Constants;
using Application.Services.AccountProfiles;
using Application.Services.Accounts;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Profiles.Rules;

public class ProfileBusinessRules : BaseBusinessRules
{
    private readonly IProfileRepository _profileRepository;
    private readonly IAccountProfilesService _accountsService;

    public ProfileBusinessRules(IProfileRepository profileRepository, IAccountProfilesService accountsService)
    {
        _profileRepository = profileRepository;
        _accountsService = accountsService;
    }

    public Task ProfileShouldExistWhenSelected(Profile? profile)
    {
        if (profile == null)
            throw new BusinessException(ProfilesBusinessMessages.ProfileNotExists);
        return Task.CompletedTask;
    }
    public Task ProfilePasswordLengthControl(string password, int controlValue)
    {
        if (password.Length != controlValue)
            throw new BusinessException(ProfilesBusinessMessages.ProfileLengthShouldBeControlled);
        return Task.CompletedTask;
    }

    public async Task ProfileIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Profile? profile = await _profileRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ProfileShouldExistWhenSelected(profile);
    }
    public async Task AccountProfileCountControl(int accountId, int count, CancellationToken cancellationToken)
    {
        var accountProfile = await _accountsService.GetListAsync(
            predicate: p => p.AccountId == accountId,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        if (accountProfile!.Count == count)
            throw new BusinessException("An account can have a maximum of 5 profiles");
    }

}