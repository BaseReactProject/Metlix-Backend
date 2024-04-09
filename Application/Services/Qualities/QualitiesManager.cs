using Application.Features.Qualities.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Qualities;

public class QualitiesManager : IQualitiesService
{
    private readonly IQualityRepository _qualityRepository;
    private readonly QualityBusinessRules _qualityBusinessRules;

    public QualitiesManager(IQualityRepository qualityRepository, QualityBusinessRules qualityBusinessRules)
    {
        _qualityRepository = qualityRepository;
        _qualityBusinessRules = qualityBusinessRules;
    }

    public async Task<Quality?> GetAsync(
        Expression<Func<Quality, bool>> predicate,
        Func<IQueryable<Quality>, IIncludableQueryable<Quality, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Quality? quality = await _qualityRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return quality;
    }

    public async Task<IPaginate<Quality>?> GetListAsync(
        Expression<Func<Quality, bool>>? predicate = null,
        Func<IQueryable<Quality>, IOrderedQueryable<Quality>>? orderBy = null,
        Func<IQueryable<Quality>, IIncludableQueryable<Quality, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Quality> qualityList = await _qualityRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return qualityList;
    }

    public async Task<Quality> AddAsync(Quality quality)
    {
        Quality addedQuality = await _qualityRepository.AddAsync(quality);

        return addedQuality;
    }

    public async Task<Quality> UpdateAsync(Quality quality)
    {
        Quality updatedQuality = await _qualityRepository.UpdateAsync(quality);

        return updatedQuality;
    }

    public async Task<Quality> DeleteAsync(Quality quality, bool permanent = false)
    {
        Quality deletedQuality = await _qualityRepository.DeleteAsync(quality);

        return deletedQuality;
    }
}
