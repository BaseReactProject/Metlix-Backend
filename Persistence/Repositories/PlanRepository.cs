using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PlanRepository : EfRepositoryBase<Plan, int, BaseDbContext>, IPlanRepository
{
    public PlanRepository(BaseDbContext context) : base(context)
    {
    }
}