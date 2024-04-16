using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ITrailerRepository : IAsyncRepository<Trailer, int>, IRepository<Trailer, int>
{
}