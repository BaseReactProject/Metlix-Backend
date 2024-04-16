using Application.Features.TrailerGenres.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.TrailerGenres.Rules;

public class TrailerGenreBusinessRules : BaseBusinessRules
{
    private readonly ITrailerGenreRepository _trailerGenreRepository;

    public TrailerGenreBusinessRules(ITrailerGenreRepository trailerGenreRepository)
    {
        _trailerGenreRepository = trailerGenreRepository;
    }

    public Task TrailerGenreShouldExistWhenSelected(TrailerGenre? trailerGenre)
    {
        if (trailerGenre == null)
            throw new BusinessException(TrailerGenresBusinessMessages.TrailerGenreNotExists);
        return Task.CompletedTask;
    }

    public async Task TrailerGenreIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        TrailerGenre? trailerGenre = await _trailerGenreRepository.GetAsync(
            predicate: tg => tg.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await TrailerGenreShouldExistWhenSelected(trailerGenre);
    }
}