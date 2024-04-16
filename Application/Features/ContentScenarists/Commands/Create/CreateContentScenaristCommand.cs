using Application.Features.ContentScenarists.Constants;
using Application.Features.ContentScenarists.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentScenarists.Constants.ContentScenaristsOperationClaims;

namespace Application.Features.ContentScenarists.Commands.Create;

public class CreateContentScenaristCommand : IRequest<CreatedContentScenaristResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int ContentId { get; set; }
    public int PersonId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentScenaristsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentScenarists";

    public class CreateContentScenaristCommandHandler : IRequestHandler<CreateContentScenaristCommand, CreatedContentScenaristResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentScenaristRepository _contentScenaristRepository;
        private readonly ContentScenaristBusinessRules _contentScenaristBusinessRules;

        public CreateContentScenaristCommandHandler(IMapper mapper, IContentScenaristRepository contentScenaristRepository,
                                         ContentScenaristBusinessRules contentScenaristBusinessRules)
        {
            _mapper = mapper;
            _contentScenaristRepository = contentScenaristRepository;
            _contentScenaristBusinessRules = contentScenaristBusinessRules;
        }

        public async Task<CreatedContentScenaristResponse> Handle(CreateContentScenaristCommand request, CancellationToken cancellationToken)
        {
            ContentScenarist contentScenarist = _mapper.Map<ContentScenarist>(request);

            await _contentScenaristRepository.AddAsync(contentScenarist);

            CreatedContentScenaristResponse response = _mapper.Map<CreatedContentScenaristResponse>(contentScenarist);
            return response;
        }
    }
}