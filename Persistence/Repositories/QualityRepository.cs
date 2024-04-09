using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class QualityRepository : EfRepositoryBase<Quality, int, BaseDbContext>, IQualityRepository
{
    public QualityRepository(BaseDbContext context) : base(context)
    {
    }
}