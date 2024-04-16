using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TrailerGenreRepository : EfRepositoryBase<TrailerGenre, int, BaseDbContext>, ITrailerGenreRepository
{
    public TrailerGenreRepository(BaseDbContext context) : base(context)
    {
    }
}