using Application.Features.ContentGenres.Constants;
using Application.Features.ContentGenres.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ContentGenres.Constants.ContentGenresOperationClaims;

namespace Application.Features.ContentGenres.Queries.GetById;

public class GetByIdContentGenreQuery : IRequest<GetByIdContentGenreResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentGenreQueryHandler : IRequestHandler<GetByIdContentGenreQuery, GetByIdContentGenreResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentGenreRepository _contentGenreRepository;
        private readonly ContentGenreBusinessRules _contentGenreBusinessRules;

        public GetByIdContentGenreQueryHandler(IMapper mapper, IContentGenreRepository contentGenreRepository, ContentGenreBusinessRules contentGenreBusinessRules)
        {
            _mapper = mapper;
            _contentGenreRepository = contentGenreRepository;
            _contentGenreBusinessRules = contentGenreBusinessRules;
        }

        public async Task<GetByIdContentGenreResponse> Handle(GetByIdContentGenreQuery request, CancellationToken cancellationToken)
        {
            ContentGenre? contentGenre = await _contentGenreRepository.GetAsync(predicate: cg => cg.Id == request.Id, cancellationToken: cancellationToken);
            await _contentGenreBusinessRules.ContentGenreShouldExistWhenSelected(contentGenre);

            GetByIdContentGenreResponse response = _mapper.Map<GetByIdContentGenreResponse>(contentGenre);
            return response;
        }
    }
}