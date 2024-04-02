using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IFakeEntityRepository : IAsyncRepository<FakeEntity, int>, IRepository<FakeEntity, int>
{
}