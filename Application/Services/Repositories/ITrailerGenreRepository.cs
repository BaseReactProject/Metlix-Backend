using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITrailerGenreRepository : IAsyncRepository<TrailerGenre, int>, IRepository<TrailerGenre, int>
{
}