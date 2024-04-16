using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContentRepository : EfRepositoryBase<Content, int, BaseDbContext>, IContentRepository
{
    public ContentRepository(BaseDbContext context) : base(context)
    {
    }
}