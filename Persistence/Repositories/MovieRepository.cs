using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class MovieRepository : EfRepositoryBase<Movie, int, BaseDbContext>, IMovieRepository
{
    public MovieRepository(BaseDbContext context) : base(context)
    {
    }
}