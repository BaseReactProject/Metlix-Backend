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

namespace Application.Features.Genres.Commands.Update;

public class UpdateGenreCommand : IRequest<UpdatedGenreResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, GenresOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetGenres";

    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, UpdatedGenreResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGenreRepository _genreRepository;
        private readonly GenreBusinessRules _genreBusinessRules;

        public UpdateGenreCommandHandler(IMapper mapper, IGenreRepository genreRepository,
                                         GenreBusinessRules genreBusinessRules)
        {
            _mapper = mapper;
            _genreRepository = genreRepository;
            _genreBusinessRules = genreBusinessRules;
        }

        public async Task<UpdatedGenreResponse> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            Genre? genre = await _genreRepository.GetAsync(predicate: g => g.Id == request.Id, cancellationToken: cancellationToken);
            await _genreBusinessRules.GenreShouldExistWhenSelected(genre);
            genre = _mapper.Map(request, genre);

            await _genreRepository.UpdateAsync(genre!);

            UpdatedGenreResponse response = _mapper.Map<UpdatedGenreResponse>(genre);
            return response;
        }
    }
}