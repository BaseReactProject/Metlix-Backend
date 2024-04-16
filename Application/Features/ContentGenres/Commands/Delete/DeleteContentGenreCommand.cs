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

namespace Application.Features.ContentGenres.Commands.Delete;

public class DeleteContentGenreCommand : IRequest<DeletedContentGenreResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentGenresOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentGenres";

    public class DeleteContentGenreCommandHandler : IRequestHandler<DeleteContentGenreCommand, DeletedContentGenreResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentGenreRepository _contentGenreRepository;
        private readonly ContentGenreBusinessRules _contentGenreBusinessRules;

        public DeleteContentGenreCommandHandler(IMapper mapper, IContentGenreRepository contentGenreRepository,
                                         ContentGenreBusinessRules contentGenreBusinessRules)
        {
            _mapper = mapper;
            _contentGenreRepository = contentGenreRepository;
            _contentGenreBusinessRules = contentGenreBusinessRules;
        }

        public async Task<DeletedContentGenreResponse> Handle(DeleteContentGenreCommand request, CancellationToken cancellationToken)
        {
            ContentGenre? contentGenre = await _contentGenreRepository.GetAsync(predicate: cg => cg.Id == request.Id, cancellationToken: cancellationToken);
            await _contentGenreBusinessRules.ContentGenreShouldExistWhenSelected(contentGenre);

            await _contentGenreRepository.DeleteAsync(contentGenre!);

            DeletedContentGenreResponse response = _mapper.Map<DeletedContentGenreResponse>(contentGenre);
            return response;
        }
    }
}