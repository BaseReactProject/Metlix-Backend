using Application.Features.Genres.Constants;
using Application.Features.Genres.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Genres.Constants.GenresOperationClaims;

namespace Application.Features.Genres.Commands.Create;

public class CreateGenreCommand : IRequest<CreatedGenreResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, GenresOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetGenres";

    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, CreatedGenreResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;
        private readonly GenreBusinessRules _genreBusinessRules;

        public CreateGenreCommandHandler(IMapper mapper, IGenreRepository genreRepository,
                                         GenreBusinessRules genreBusinessRules)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
            _genreBusinessRules = genreBusinessRules;
        }

        public async Task<CreatedGenreResponse> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            Genre genre = _mapper.Map<Genre>(request);

            await _genreRepository.AddAsync(genre);

            CreatedGenreResponse response = _mapper.Map<CreatedGenreResponse>(genre);
            return response;
        }
    }
}