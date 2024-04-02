using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class FakeEntityRepository : EfRepositoryBase<FakeEntity, int, BaseDbContext>, IFakeEntityRepository
{
    public FakeEntityRepository(BaseDbContext context) : base(context)
    {
    }
}