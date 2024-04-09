using Application.Features.Qualities.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Qualities.Rules;

public class QualityBusinessRules : BaseBusinessRules
{
    private readonly IQualityRepository _qualityRepository;

    public QualityBusinessRules(IQualityRepository qualityRepository)
    {
        _qualityRepository = qualityRepository;
    }

    public Task QualityShouldExistWhenSelected(Quality? quality)
    {
        if (quality == null)
            throw new BusinessException(QualitiesBusinessMessages.QualityNotExists);
        return Task.CompletedTask;
    }

    public async Task QualityIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Quality? quality = await _qualityRepository.GetAsync(
            predicate: q => q.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await QualityShouldExistWhenSelected(quality);
    }
}