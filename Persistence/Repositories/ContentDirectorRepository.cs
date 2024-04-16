using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContentDirectorRepository : EfRepositoryBase<ContentDirector, int, BaseDbContext>, IContentDirectorRepository
{
    public ContentDirectorRepository(BaseDbContext context) : base(context)
    {
    }
}