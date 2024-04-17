using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContentIntroRepository : EfRepositoryBase<ContentIntro, int, BaseDbContext>, IContentIntroRepository
{
    public ContentIntroRepository(BaseDbContext context) : base(context)
    {
    }
}