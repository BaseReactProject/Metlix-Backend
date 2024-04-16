using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContentGenreRepository : EfRepositoryBase<ContentGenre, int, BaseDbContext>, IContentGenreRepository
{
    public ContentGenreRepository(BaseDbContext context) : base(context)
    {
    }
}