using Application.Features.Plans.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Plans;

public class PlansManager : IPlansService
{
    private readonly IPlanRepository _planRepository;
    private readonly PlanBusinessRules _planBusinessRules;

    public PlansManager(IPlanRepository planRepository, PlanBusinessRules planBusinessRules)
    {
        _planRepository = planRepository;
        _planBusinessRules = planBusinessRules;
    }

    public async Task<Plan?> GetAsync(
        Expression<Func<Plan, bool>> predicate,
        Func<IQueryable<Plan>, IIncludableQueryable<Plan, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Plan? plan = await _planRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return plan;
    }

    public async Task<IPaginate<Plan>?> GetListAsync(
        Expression<Func<Plan, bool>>? predicate = null,
        Func<IQueryable<Plan>, IOrderedQueryable<Plan>>? orderBy = null,
        Func<IQueryable<Plan>, IIncludableQueryable<Plan, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Plan> planList = await _planRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return planList;
    }

    public async Task<Plan> AddAsync(Plan plan)
    {
        Plan addedPlan = await _planRepository.AddAsync(plan);

        return addedPlan;
    }

    public async Task<Plan> UpdateAsync(Plan plan)
    {
        Plan updatedPlan = await _planRepository.UpdateAsync(plan);

        return updatedPlan;
    }

    public async Task<Plan> DeleteAsync(Plan plan, bool permanent = false)
    {
        Plan deletedPlan = await _planRepository.DeleteAsync(plan);

        return deletedPlan;
    }
}
