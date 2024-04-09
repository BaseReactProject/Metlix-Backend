using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AccountProfileRepository : EfRepositoryBase<AccountProfile, int, BaseDbContext>, IAccountProfileRepository
{
    public AccountProfileRepository(BaseDbContext context) : base(context)
    {
    }
}