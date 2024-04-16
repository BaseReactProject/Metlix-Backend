using Application.Features.ContentActors.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ContentActors;

public class ContentActorsManager : IContentActorsService
{
    private readonly IContentActorRepository _contentActorRepository;
    private readonly ContentActorBusinessRules _contentActorBusinessRules;

    public ContentActorsManager(IContentActorRepository contentActorRepository, ContentActorBusinessRules contentActorBusinessRules)
    {
        _contentActorRepository = contentActorRepository;
        _contentActorBusinessRules = contentActorBusinessRules;
    }

    public async Task<ContentActor?> GetAsync(
        Expression<Func<ContentActor, bool>> predicate,
        Func<IQueryable<ContentActor>, IIncludableQueryable<ContentActor, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ContentActor? contentActor = await _contentActorRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return contentActor;
    }

    public async Task<IPaginate<ContentActor>?> GetListAsync(
        Expression<Func<ContentActor, bool>>? predicate = null,
        Func<IQueryable<ContentActor>, IOrderedQueryable<ContentActor>>? orderBy = null,
        Func<IQueryable<ContentActor>, IIncludableQueryable<ContentActor, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ContentActor> contentActorList = await _contentActorRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return contentActorList;
    }

    public async Task<ContentActor> AddAsync(ContentActor contentActor)
    {
        ContentActor addedContentActor = await _contentActorRepository.AddAsync(contentActor);

        return addedContentActor;
    }

    public async Task<ContentActor> UpdateAsync(ContentActor contentActor)
    {
        ContentActor updatedContentActor = await _contentActorRepository.UpdateAsync(contentActor);

        return updatedContentActor;
    }

    public async Task<ContentActor> DeleteAsync(ContentActor contentActor, bool permanent = false)
    {
        ContentActor deletedContentActor = await _contentActorRepository.DeleteAsync(contentActor);

        return deletedContentActor;
    }
}
