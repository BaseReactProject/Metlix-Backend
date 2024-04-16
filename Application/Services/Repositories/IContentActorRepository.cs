using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IContentActorRepository : IAsyncRepository<ContentActor, int>, IRepository<ContentActor, int>
{
}