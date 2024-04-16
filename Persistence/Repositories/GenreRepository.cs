using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class GenreRepository : EfRepositoryBase<Genre, int, BaseDbContext>, IGenreRepository
{
    public GenreRepository(BaseDbContext context) : base(context)
    {
    }
}