using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TrailerRepository : EfRepositoryBase<Trailer, int, BaseDbContext>, ITrailerRepository
{
    public TrailerRepository(BaseDbContext context) : base(context)
    {
    }
}