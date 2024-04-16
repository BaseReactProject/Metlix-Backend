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
using Application.Features.Constants;
using Application.Features.OperationClaims.Constants;

namespace Application.Features.Movies.Commands.Create;

public class CreateMovieCommand : IRequest<CreatedMovieResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string MovieUrl { get; set; }

    public string[] Roles => new[] { GeneralOperationClaims.Admin };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetMovies";

    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, CreatedMovieResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly MovieBusinessRules _movieBusinessRules;

        public CreateMovieCommandHandler(IMapper mapper, IMovieRepository movieRepository,
                                         MovieBusinessRules movieBusinessRules)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _movieBusinessRules = movieBusinessRules;
        }

        public async Task<CreatedMovieResponse> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            Movie movie = _mapper.Map<Movie>(request);

            await _movieRepository.AddAsync(movie);

            CreatedMovieResponse response = _mapper.Map<CreatedMovieResponse>(movie);
            return response;
        }
    }
}