using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContentOutroRepository : EfRepositoryBase<ContentOutro, int, BaseDbContext>, IContentOutroRepository
{
    public ContentOutroRepository(BaseDbContext context) : base(context)
    {
    }
}