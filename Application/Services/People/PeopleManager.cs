using Application.Features.People.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.People;

public class PeopleManager : IPeopleService
{
    private readonly IPersonRepository _personRepository;
    private readonly PersonBusinessRules _personBusinessRules;

    public PeopleManager(IPersonRepository personRepository, PersonBusinessRules personBusinessRules)
    {
        _personRepository = personRepository;
        _personBusinessRules = personBusinessRules;
    }

    public async Task<Person?> GetAsync(
        Expression<Func<Person, bool>> predicate,
        Func<IQueryable<Person>, IIncludableQueryable<Person, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Person? person = await _personRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return person;
    }

    public async Task<IPaginate<Person>?> GetListAsync(
        Expression<Func<Person, bool>>? predicate = null,
        Func<IQueryable<Person>, IOrderedQueryable<Person>>? orderBy = null,
        Func<IQueryable<Person>, IIncludableQueryable<Person, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Person> personList = await _personRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return personList;
    }

    public async Task<Person> AddAsync(Person person)
    {
        Person addedPerson = await _personRepository.AddAsync(person);

        return addedPerson;
    }

    public async Task<Person> UpdateAsync(Person person)
    {
        Person updatedPerson = await _personRepository.UpdateAsync(person);

        return updatedPerson;
    }

    public async Task<Person> DeleteAsync(Person person, bool permanent = false)
    {
        Person deletedPerson = await _personRepository.DeleteAsync(person);

        return deletedPerson;
    }
}
