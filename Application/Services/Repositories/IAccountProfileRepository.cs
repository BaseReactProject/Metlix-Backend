using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAccountProfileRepository : IAsyncRepository<AccountProfile, int>, IRepository<AccountProfile, int>
{
}