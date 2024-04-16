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

namespace Application.Features.TrailerGenres.Commands.Update;

public class UpdateTrailerGenreCommand : IRequest<UpdatedTrailerGenreResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int TrailerId { get; set; }
    public int GenreId { get; set; }

    public string[] Roles => new[] { Admin, Write, TrailerGenresOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetTrailerGenres";

    public class UpdateTrailerGenreCommandHandler : IRequestHandler<UpdateTrailerGenreCommand, UpdatedTrailerGenreResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITrailerGenreRepository _trailerGenreRepository;
        private readonly TrailerGenreBusinessRules _trailerGenreBusinessRules;

        public UpdateTrailerGenreCommandHandler(IMapper mapper, ITrailerGenreRepository trailerGenreRepository,
                                         TrailerGenreBusinessRules trailerGenreBusinessRules)
        {
            _mapper = mapper;
            _trailerGenreRepository = trailerGenreRepository;
            _trailerGenreBusinessRules = trailerGenreBusinessRules;
        }

        public async Task<UpdatedTrailerGenreResponse> Handle(UpdateTrailerGenreCommand request, CancellationToken cancellationToken)
        {
            TrailerGenre? trailerGenre = await _trailerGenreRepository.GetAsync(predicate: tg => tg.Id == request.Id, cancellationToken: cancellationToken);
            await _trailerGenreBusinessRules.TrailerGenreShouldExistWhenSelected(trailerGenre);
            trailerGenre = _mapper.Map(request, trailerGenre);

            await _trailerGenreRepository.UpdateAsync(trailerGenre!);

            UpdatedTrailerGenreResponse response = _mapper.Map<UpdatedTrailerGenreResponse>(trailerGenre);
            return response;
        }
    }
}