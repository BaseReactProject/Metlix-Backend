using Application.Features.TrailerGenres.Constants;
using Application.Features.TrailerGenres.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.TrailerGenres.Constants.TrailerGenresOperationClaims;

namespace Application.Features.TrailerGenres.Commands.Create;

public class CreateTrailerGenreCommand : IRequest<CreatedTrailerGenreResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int TrailerId { get; set; }
    public int GenreId { get; set; }

    public string[] Roles => new[] { Admin, Write, TrailerGenresOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetTrailerGenres";

    public class CreateTrailerGenreCommandHandler : IRequestHandler<CreateTrailerGenreCommand, CreatedTrailerGenreResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITrailerGenreRepository _trailerGenreRepository;
        private readonly TrailerGenreBusinessRules _trailerGenreBusinessRules;

        public CreateTrailerGenreCommandHandler(IMapper mapper, ITrailerGenreRepository trailerGenreRepository,
                                         TrailerGenreBusinessRules trailerGenreBusinessRules)
        {
            _mapper = mapper;
            _trailerGenreRepository = trailerGenreRepository;
            _trailerGenreBusinessRules = trailerGenreBusinessRules;
        }

        public async Task<CreatedTrailerGenreResponse> Handle(CreateTrailerGenreCommand request, CancellationToken cancellationToken)
        {
            TrailerGenre trailerGenre = _mapper.Map<TrailerGenre>(request);

            await _trailerGenreRepository.AddAsync(trailerGenre);

            CreatedTrailerGenreResponse response = _mapper.Map<CreatedTrailerGenreResponse>(trailerGenre);
            return response;
        }
    }
}