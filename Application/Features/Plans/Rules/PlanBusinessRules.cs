using Application.Features.Plans.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Plans.Rules;

public class PlanBusinessRules : BaseBusinessRules
{
    private readonly IPlanRepository _planRepository;

    public PlanBusinessRules(IPlanRepository planRepository)
    {
        _planRepository = planRepository;
    }

    public Task PlanShouldExistWhenSelected(Plan? plan)
    {
        if (plan == null)
            throw new BusinessException(PlansBusinessMessages.PlanNotExists);
        return Task.CompletedTask;
    }

    public async Task PlanIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Plan? plan = await _planRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PlanShouldExistWhenSelected(plan);
    }
}