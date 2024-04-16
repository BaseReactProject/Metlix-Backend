using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentGenreRepository : IAsyncRepository<ContentGenre, int>, IRepository<ContentGenre, int>
{
}