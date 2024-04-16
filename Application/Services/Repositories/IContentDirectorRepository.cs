using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentDirectorRepository : IAsyncRepository<ContentDirector, int>, IRepository<ContentDirector, int>
{
}