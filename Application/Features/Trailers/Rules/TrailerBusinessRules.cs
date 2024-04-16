using Application.Features.Trailers.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Trailers.Rules;

public class TrailerBusinessRules : BaseBusinessRules
{
    private readonly ITrailerRepository _trailerRepository;

    public TrailerBusinessRules(ITrailerRepository trailerRepository)
    {
        _trailerRepository = trailerRepository;
    }

    public Task TrailerShouldExistWhenSelected(Trailer? trailer)
    {
        if (trailer == null)
            throw new BusinessException(TrailersBusinessMessages.TrailerNotExists);
        return Task.CompletedTask;
    }

    public async Task TrailerIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Trailer? trailer = await _trailerRepository.GetAsync(
            predicate: t => t.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TrailerShouldExistWhenSelected(trailer);
    }
}