using Application.Features.Movies.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Movies.Rules;

public class MovieBusinessRules : BaseBusinessRules
{
    private readonly IMovieRepository _movieRepository;

    public MovieBusinessRules(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public Task MovieShouldExistWhenSelected(Movie? movie)
    {
        if (movie == null)
            throw new BusinessException(MoviesBusinessMessages.MovieNotExists);
        return Task.CompletedTask;
    }

    public async Task MovieIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Movie? movie = await _movieRepository.GetAsync(
            predicate: m => m.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await MovieShouldExistWhenSelected(movie);
    }
}