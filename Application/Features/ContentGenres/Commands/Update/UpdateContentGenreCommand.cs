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

namespace Application.Features.ContentGenres.Commands.Update;

public class UpdateContentGenreCommand : IRequest<UpdatedContentGenreResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int GenreId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentGenresOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentGenres";

    public class UpdateContentGenreCommandHandler : IRequestHandler<UpdateContentGenreCommand, UpdatedContentGenreResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentGenreRepository _contentGenreRepository;
        private readonly ContentGenreBusinessRules _contentGenreBusinessRules;

        public UpdateContentGenreCommandHandler(IMapper mapper, IContentGenreRepository contentGenreRepository,
                                         ContentGenreBusinessRules contentGenreBusinessRules)
        {
            _mapper = mapper;
            _contentGenreRepository = contentGenreRepository;
            _contentGenreBusinessRules = contentGenreBusinessRules;
        }

        public async Task<UpdatedContentGenreResponse> Handle(UpdateContentGenreCommand request, CancellationToken cancellationToken)
        {
            ContentGenre? contentGenre = await _contentGenreRepository.GetAsync(predicate: cg => cg.Id == request.Id, cancellationToken: cancellationToken);
            await _contentGenreBusinessRules.ContentGenreShouldExistWhenSelected(contentGenre);
            contentGenre = _mapper.Map(request, contentGenre);

            await _contentGenreRepository.UpdateAsync(contentGenre!);

            UpdatedContentGenreResponse response = _mapper.Map<UpdatedContentGenreResponse>(contentGenre);
            return response;
        }
    }
}