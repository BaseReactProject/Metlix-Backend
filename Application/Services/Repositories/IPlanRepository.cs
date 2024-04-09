using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IPlanRepository : IAsyncRepository<Plan, int>, IRepository<Plan, int>
{
}