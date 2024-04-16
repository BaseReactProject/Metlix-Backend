using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IPersonRepository : IAsyncRepository<Person, int>, IRepository<Person, int>
{
}