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

namespace Application.Features.ContentTrailers.Commands.Create;

public class CreateContentTrailerCommand : IRequest<CreatedContentTrailerResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int TrailerId { get; set; }
    public int ContentId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentTrailersOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentTrailers";

    public class CreateContentTrailerCommandHandler : IRequestHandler<CreateContentTrailerCommand, CreatedContentTrailerResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentTrailerRepository _contentTrailerRepository;
        private readonly ContentTrailerBusinessRules _contentTrailerBusinessRules;

        public CreateContentTrailerCommandHandler(IMapper mapper, IContentTrailerRepository contentTrailerRepository,
                                         ContentTrailerBusinessRules contentTrailerBusinessRules)
        {
            _mapper = mapper;
            _contentTrailerRepository = contentTrailerRepository;
            _contentTrailerBusinessRules = contentTrailerBusinessRules;
        }

        public async Task<CreatedContentTrailerResponse> Handle(CreateContentTrailerCommand request, CancellationToken cancellationToken)
        {
            ContentTrailer contentTrailer = _mapper.Map<ContentTrailer>(request);

            await _contentTrailerRepository.AddAsync(contentTrailer);

            CreatedContentTrailerResponse response = _mapper.Map<CreatedContentTrailerResponse>(contentTrailer);
            return response;
        }
    }
}