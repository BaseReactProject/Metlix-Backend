using Application.Features.ContentActors.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContentActors.Rules;

public class ContentActorBusinessRules : BaseBusinessRules
{
    private readonly IContentActorRepository _contentActorRepository;

    public ContentActorBusinessRules(IContentActorRepository contentActorRepository)
    {
        _contentActorRepository = contentActorRepository;
    }

    public Task ContentActorShouldExistWhenSelected(ContentActor? contentActor)
    {
        if (contentActor == null)
            throw new BusinessException(ContentActorsBusinessMessages.ContentActorNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentActorIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        ContentActor? contentActor = await _contentActorRepository.GetAsync(
            predicate: ca => ca.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentActorShouldExistWhenSelected(contentActor);
    }
}