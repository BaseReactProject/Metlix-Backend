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

namespace Application.Features.ContentScenarists.Commands.Delete;

public class DeleteContentScenaristCommand : IRequest<DeletedContentScenaristResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentScenaristsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentScenarists";

    public class DeleteContentScenaristCommandHandler : IRequestHandler<DeleteContentScenaristCommand, DeletedContentScenaristResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentScenaristRepository _contentScenaristRepository;
        private readonly ContentScenaristBusinessRules _contentScenaristBusinessRules;

        public DeleteContentScenaristCommandHandler(IMapper mapper, IContentScenaristRepository contentScenaristRepository,
                                         ContentScenaristBusinessRules contentScenaristBusinessRules)
        {
            _mapper = mapper;
            _contentScenaristRepository = contentScenaristRepository;
            _contentScenaristBusinessRules = contentScenaristBusinessRules;
        }

        public async Task<DeletedContentScenaristResponse> Handle(DeleteContentScenaristCommand request, CancellationToken cancellationToken)
        {
            ContentScenarist? contentScenarist = await _contentScenaristRepository.GetAsync(predicate: cs => cs.Id == request.Id, cancellationToken: cancellationToken);
            await _contentScenaristBusinessRules.ContentScenaristShouldExistWhenSelected(contentScenarist);

            await _contentScenaristRepository.DeleteAsync(contentScenarist!);

            DeletedContentScenaristResponse response = _mapper.Map<DeletedContentScenaristResponse>(contentScenarist);
            return response;
        }
    }
}