using Application.Features.Genres.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Genres.Rules;

public class GenreBusinessRules : BaseBusinessRules
{
    private readonly IGenreRepository _genreRepository;

    public GenreBusinessRules(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public Task GenreShouldExistWhenSelected(Genre? genre)
    {
        if (genre == null)
            throw new BusinessException(GenresBusinessMessages.GenreNotExists);
        return Task.CompletedTask;
    }

    public async Task GenreIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Genre? genre = await _genreRepository.GetAsync(
            predicate: g => g.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await GenreShouldExistWhenSelected(genre);
    }
}