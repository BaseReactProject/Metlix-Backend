using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IMovieRepository : IAsyncRepository<Movie, int>, IRepository<Movie, int>
{
}