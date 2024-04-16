using Application.Features.ContentGenres.Constants;
using Application.Features.ContentGenres.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentGenres.Constants.ContentGenresOperationClaims;

namespace Application.Features.ContentGenres.Commands.Create;

public class CreateContentGenreCommand : IRequest<CreatedContentGenreResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int ContentId { get; set; }
    public int GenreId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentGenresOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentGenres";

    public class CreateContentGenreCommandHandler : IRequestHandler<CreateContentGenreCommand, CreatedContentGenreResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentGenreRepository _contentGenreRepository;
        private readonly ContentGenreBusinessRules _contentGenreBusinessRules;

        public CreateContentGenreCommandHandler(IMapper mapper, IContentGenreRepository contentGenreRepository,
                                         ContentGenreBusinessRules contentGenreBusinessRules)
        {
            _mapper = mapper;
            _contentGenreRepository = contentGenreRepository;
            _contentGenreBusinessRules = contentGenreBusinessRules;
        }

        public async Task<CreatedContentGenreResponse> Handle(CreateContentGenreCommand request, CancellationToken cancellationToken)
        {
            ContentGenre contentGenre = _mapper.Map<ContentGenre>(request);

            await _contentGenreRepository.AddAsync(contentGenre);

            CreatedContentGenreResponse response = _mapper.Map<CreatedContentGenreResponse>(contentGenre);
            return response;
        }
    }
}