using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.People;

public interface IPeopleService
{
    Task<Person?> GetAsync(
        Expression<Func<Person, bool>> predicate,
        Func<IQueryable<Person>, IIncludableQueryable<Person, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Person>?> GetListAsync(
        Expression<Func<Person, bool>>? predicate = null,
        Func<IQueryable<Person>, IOrderedQueryable<Person>>? orderBy = null,
        Func<IQueryable<Person>, IIncludableQueryable<Person, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Person> AddAsync(Person person);
    Task<Person> UpdateAsync(Person person);
    Task<Person> DeleteAsync(Person person, bool permanent = false);
}
