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

namespace Application.Features.ContentScenarists.Commands.Update;

public class UpdateContentScenaristCommand : IRequest<UpdatedContentScenaristResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentScenaristsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentScenarists";

    public class UpdateContentScenaristCommandHandler : IRequestHandler<UpdateContentScenaristCommand, UpdatedContentScenaristResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentScenaristRepository _contentScenaristRepository;
        private readonly ContentScenaristBusinessRules _contentScenaristBusinessRules;

        public UpdateContentScenaristCommandHandler(IMapper mapper, IContentScenaristRepository contentScenaristRepository,
                                         ContentScenaristBusinessRules contentScenaristBusinessRules)
        {
            _mapper = mapper;
            _contentScenaristRepository = contentScenaristRepository;
            _contentScenaristBusinessRules = contentScenaristBusinessRules;
        }

        public async Task<UpdatedContentScenaristResponse> Handle(UpdateContentScenaristCommand request, CancellationToken cancellationToken)
        {
            ContentScenarist? contentScenarist = await _contentScenaristRepository.GetAsync(predicate: cs => cs.Id == request.Id, cancellationToken: cancellationToken);
            await _contentScenaristBusinessRules.ContentScenaristShouldExistWhenSelected(contentScenarist);
            contentScenarist = _mapper.Map(request, contentScenarist);

            await _contentScenaristRepository.UpdateAsync(contentScenarist!);

            UpdatedContentScenaristResponse response = _mapper.Map<UpdatedContentScenaristResponse>(contentScenarist);
            return response;
        }
    }
}