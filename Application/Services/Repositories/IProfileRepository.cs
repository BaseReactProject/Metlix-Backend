using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IProfileRepository : IAsyncRepository<Profile, int>, IRepository<Profile, int>
{
}