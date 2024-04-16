using Application.Features.ContentTrailers.Constants;
using Application.Features.ContentTrailers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentTrailers.Constants.ContentTrailersOperationClaims;

namespace Application.Features.ContentTrailers.Commands.Delete;

public class DeleteContentTrailerCommand : IRequest<DeletedContentTrailerResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentTrailersOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentTrailers";

    public class DeleteContentTrailerCommandHandler : IRequestHandler<DeleteContentTrailerCommand, DeletedContentTrailerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentTrailerRepository _contentTrailerRepository;
        private readonly ContentTrailerBusinessRules _contentTrailerBusinessRules;

        public DeleteContentTrailerCommandHandler(IMapper mapper, IContentTrailerRepository contentTrailerRepository,
                                         ContentTrailerBusinessRules contentTrailerBusinessRules)
        {
            _mapper = mapper;
            _contentTrailerRepository = contentTrailerRepository;
            _contentTrailerBusinessRules = contentTrailerBusinessRules;
        }

        public async Task<DeletedContentTrailerResponse> Handle(DeleteContentTrailerCommand request, CancellationToken cancellationToken)
        {
            ContentTrailer? contentTrailer = await _contentTrailerRepository.GetAsync(predicate: ct => ct.Id == request.Id, cancellationToken: cancellationToken);
            await _contentTrailerBusinessRules.ContentTrailerShouldExistWhenSelected(contentTrailer);

            await _contentTrailerRepository.DeleteAsync(contentTrailer!);

            DeletedContentTrailerResponse response = _mapper.Map<DeletedContentTrailerResponse>(contentTrailer);
            return response;
        }
    }
}