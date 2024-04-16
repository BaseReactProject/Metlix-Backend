using Application.Features.People.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.People.Rules;

public class PersonBusinessRules : BaseBusinessRules
{
    private readonly IPersonRepository _personRepository;

    public PersonBusinessRules(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public Task PersonShouldExistWhenSelected(Person? person)
    {
        if (person == null)
            throw new BusinessException(PeopleBusinessMessages.PersonNotExists);
        return Task.CompletedTask;
    }

    public async Task PersonIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Person? person = await _personRepository.GetAsync(
            predicate: p => p.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await PersonShouldExistWhenSelected(person);
    }
}