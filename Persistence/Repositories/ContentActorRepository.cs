using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContentActorRepository : EfRepositoryBase<ContentActor, int, BaseDbContext>, IContentActorRepository
{
    public ContentActorRepository(BaseDbContext context) : base(context)
    {
    }
}