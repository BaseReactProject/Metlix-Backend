using Application.Features.Movies.Constants;
using Application.Features.Movies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Movies.Constants.MoviesOperationClaims;

namespace Application.Features.Movies.Commands.Update;

public class UpdateMovieCommand : IRequest<UpdatedMovieResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string MovieUrl { get; set; }

    public string[] Roles => new[] { Admin, Write, MoviesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetMovies";

    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, UpdatedMovieResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly MovieBusinessRules _movieBusinessRules;

        public UpdateMovieCommandHandler(IMapper mapper, IMovieRepository movieRepository,
                                         MovieBusinessRules movieBusinessRules)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _movieBusinessRules = movieBusinessRules;
        }

        public async Task<UpdatedMovieResponse> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            Movie? movie = await _movieRepository.GetAsync(predicate: m => m.Id == request.Id, cancellationToken: cancellationToken);
            await _movieBusinessRules.MovieShouldExistWhenSelected(movie);
            movie = _mapper.Map(request, movie);

            await _movieRepository.UpdateAsync(movie!);

            UpdatedMovieResponse response = _mapper.Map<UpdatedMovieResponse>(movie);
            return response;
        }
    }
}