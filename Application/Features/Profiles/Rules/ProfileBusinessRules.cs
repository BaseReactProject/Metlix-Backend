using Application.Features.Profiles.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Profiles.Rules;

public class ProfileBusinessRules : BaseBusinessRules
{
    private readonly IProfileRepository _profileRepository;

    public ProfileBusinessRules(IProfileRepository profileRepository)
    {
        _profileRepository = profileRepository;
    }

    public Task ProfileShouldExistWhenSelected(Profile? profile)
    {
        if (profile == null)
            throw new BusinessException(ProfilesBusinessMessages.ProfileNotExists);
        return Task.CompletedTask;
    }
    public Task ProfilePasswordLengthControl(string password,int controlValue)
    {
        if (password.Length!= controlValue)
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
}