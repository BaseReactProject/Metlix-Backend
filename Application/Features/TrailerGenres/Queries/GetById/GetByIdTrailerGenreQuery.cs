using Application.Features.TrailerGenres.Constants;
using Application.Features.TrailerGenres.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.TrailerGenres.Constants.TrailerGenresOperationClaims;

namespace Application.Features.TrailerGenres.Queries.GetById;

public class GetByIdTrailerGenreQuery : IRequest<GetByIdTrailerGenreResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdTrailerGenreQueryHandler : IRequestHandler<GetByIdTrailerGenreQuery, GetByIdTrailerGenreResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITrailerGenreRepository _trailerGenreRepository;
        private readonly TrailerGenreBusinessRules _trailerGenreBusinessRules;

        public GetByIdTrailerGenreQueryHandler(IMapper mapper, ITrailerGenreRepository trailerGenreRepository, TrailerGenreBusinessRules trailerGenreBusinessRules)
        {
            _mapper = mapper;
            _trailerGenreRepository = trailerGenreRepository;
            _trailerGenreBusinessRules = trailerGenreBusinessRules;
        }

        public async Task<GetByIdTrailerGenreResponse> Handle(GetByIdTrailerGenreQuery request, CancellationToken cancellationToken)
        {
            TrailerGenre? trailerGenre = await _trailerGenreRepository.GetAsync(predicate: tg => tg.Id == request.Id, cancellationToken: cancellationToken);
            await _trailerGenreBusinessRules.TrailerGenreShouldExistWhenSelected(trailerGenre);

            GetByIdTrailerGenreResponse response = _mapper.Map<GetByIdTrailerGenreResponse>(trailerGenre);
            return response;
        }
    }
}