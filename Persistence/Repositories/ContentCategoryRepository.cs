using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContentCategoryRepository : EfRepositoryBase<ContentCategory, int, BaseDbContext>, IContentCategoryRepository
{
    public ContentCategoryRepository(BaseDbContext context) : base(context)
    {
    }
}