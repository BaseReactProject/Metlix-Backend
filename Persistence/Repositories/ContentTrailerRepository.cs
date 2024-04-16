using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ContentTrailerRepository : EfRepositoryBase<ContentTrailer, int, BaseDbContext>, IContentTrailerRepository
{
    public ContentTrailerRepository(BaseDbContext context) : base(context)
    {
    }
}