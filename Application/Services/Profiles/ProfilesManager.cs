using Application.Features.Profiles.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Profiles;

public class ProfilesManager : IProfilesService
{
    private readonly IProfileRepository _profileRepository;
    private readonly ProfileBusinessRules _profileBusinessRules;

    public ProfilesManager(IProfileRepository profileRepository, ProfileBusinessRules profileBusinessRules)
    {
        _profileRepository = profileRepository;
        _profileBusinessRules = profileBusinessRules;
    }

    public async Task<Profile?> GetAsync(
        Expression<Func<Profile, bool>> predicate,
        Func<IQueryable<Profile>, IIncludableQueryable<Profile, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Profile? profile = await _profileRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return profile;
    }

    public async Task<IPaginate<Profile>?> GetListAsync(
        Expression<Func<Profile, bool>>? predicate = null,
        Func<IQueryable<Profile>, IOrderedQueryable<Profile>>? orderBy = null,
        Func<IQueryable<Profile>, IIncludableQueryable<Profile, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Profile> profileList = await _profileRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return profileList;
    }

    public async Task<Profile> AddAsync(Profile profile)
    {
        Profile addedProfile = await _profileRepository.AddAsync(profile);

        return addedProfile;
    }

    public async Task<Profile> UpdateAsync(Profile profile)
    {
        Profile updatedProfile = await _profileRepository.UpdateAsync(profile);

        return updatedProfile;
    }

    public async Task<Profile> DeleteAsync(Profile profile, bool permanent = false)
    {
        Profile deletedProfile = await _profileRepository.DeleteAsync(profile);

        return deletedProfile;
    }
}
