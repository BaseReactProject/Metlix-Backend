using Application.Features.ContentGenres.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ContentGenres.Rules;

public class ContentGenreBusinessRules : BaseBusinessRules
{
    private readonly IContentGenreRepository _contentGenreRepository;

    public ContentGenreBusinessRules(IContentGenreRepository contentGenreRepository)
    {
        _contentGenreRepository = contentGenreRepository;
    }

    public Task ContentGenreShouldExistWhenSelected(ContentGenre? contentGenre)
    {
        if (contentGenre == null)
            throw new BusinessException(ContentGenresBusinessMessages.ContentGenreNotExists);
        return Task.CompletedTask;
    }

    public async Task ContentGenreIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        ContentGenre? contentGenre = await _contentGenreRepository.GetAsync(
            predicate: cg => cg.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ContentGenreShouldExistWhenSelected(contentGenre);
    }
}