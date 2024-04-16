using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class PersonRepository : EfRepositoryBase<Person, int, BaseDbContext>, IPersonRepository
{
    public PersonRepository(BaseDbContext context) : base(context)
    {
    }
}