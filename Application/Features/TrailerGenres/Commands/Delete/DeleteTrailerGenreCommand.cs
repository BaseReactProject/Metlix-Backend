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

namespace Application.Features.TrailerGenres.Commands.Delete;

public class DeleteTrailerGenreCommand : IRequest<DeletedTrailerGenreResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, TrailerGenresOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetTrailerGenres";

    public class DeleteTrailerGenreCommandHandler : IRequestHandler<DeleteTrailerGenreCommand, DeletedTrailerGenreResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITrailerGenreRepository _trailerGenreRepository;
        private readonly TrailerGenreBusinessRules _trailerGenreBusinessRules;

        public DeleteTrailerGenreCommandHandler(IMapper mapper, ITrailerGenreRepository trailerGenreRepository,
                                         TrailerGenreBusinessRules trailerGenreBusinessRules)
        {
            _mapper = mapper;
            _trailerGenreRepository = trailerGenreRepository;
            _trailerGenreBusinessRules = trailerGenreBusinessRules;
        }

        public async Task<DeletedTrailerGenreResponse> Handle(DeleteTrailerGenreCommand request, CancellationToken cancellationToken)
        {
            TrailerGenre? trailerGenre = await _trailerGenreRepository.GetAsync(predicate: tg => tg.Id == request.Id, cancellationToken: cancellationToken);
            await _trailerGenreBusinessRules.TrailerGenreShouldExistWhenSelected(trailerGenre);

            await _trailerGenreRepository.DeleteAsync(trailerGenre!);

            DeletedTrailerGenreResponse response = _mapper.Map<DeletedTrailerGenreResponse>(trailerGenre);
            return response;
        }
    }
}