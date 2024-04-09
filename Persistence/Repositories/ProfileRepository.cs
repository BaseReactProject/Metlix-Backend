using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProfileRepository : EfRepositoryBase<Profile, int, BaseDbContext>, IProfileRepository
{
    public ProfileRepository(BaseDbContext context) : base(context)
    {
    }
}