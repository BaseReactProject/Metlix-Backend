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

namespace Application.Features.ContentTrailers.Commands.Update;

public class UpdateContentTrailerCommand : IRequest<UpdatedContentTrailerResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int TrailerId { get; set; }
    public int ContentId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentTrailersOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentTrailers";

    public class UpdateContentTrailerCommandHandler : IRequestHandler<UpdateContentTrailerCommand, UpdatedContentTrailerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentTrailerRepository _contentTrailerRepository;
        private readonly ContentTrailerBusinessRules _contentTrailerBusinessRules;

        public UpdateContentTrailerCommandHandler(IMapper mapper, IContentTrailerRepository contentTrailerRepository,
                                         ContentTrailerBusinessRules contentTrailerBusinessRules)
        {
            _mapper = mapper;
            _contentTrailerRepository = contentTrailerRepository;
            _contentTrailerBusinessRules = contentTrailerBusinessRules;
        }

        public async Task<UpdatedContentTrailerResponse> Handle(UpdateContentTrailerCommand request, CancellationToken cancellationToken)
        {
            ContentTrailer? contentTrailer = await _contentTrailerRepository.GetAsync(predicate: ct => ct.Id == request.Id, cancellationToken: cancellationToken);
            await _contentTrailerBusinessRules.ContentTrailerShouldExistWhenSelected(contentTrailer);
            contentTrailer = _mapper.Map(request, contentTrailer);

            await _contentTrailerRepository.UpdateAsync(contentTrailer!);

            UpdatedContentTrailerResponse response = _mapper.Map<UpdatedContentTrailerResponse>(contentTrailer);
            return response;
        }
    }
}