using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IGenreRepository : IAsyncRepository<Genre, int>, IRepository<Genre, int>
{
}