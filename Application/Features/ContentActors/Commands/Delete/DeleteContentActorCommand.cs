using Application.Features.ContentActors.Constants;
using Application.Features.ContentActors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ContentActors.Constants.ContentActorsOperationClaims;

namespace Application.Features.ContentActors.Commands.Delete;

public class DeleteContentActorCommand : IRequest<DeletedContentActorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentActorsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentActors";

    public class DeleteContentActorCommandHandler : IRequestHandler<DeleteContentActorCommand, DeletedContentActorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentActorRepository _contentActorRepository;
        private readonly ContentActorBusinessRules _contentActorBusinessRules;

        public DeleteContentActorCommandHandler(IMapper mapper, IContentActorRepository contentActorRepository,
                                         ContentActorBusinessRules contentActorBusinessRules)
        {
            _mapper = mapper;
            _contentActorRepository = contentActorRepository;
            _contentActorBusinessRules = contentActorBusinessRules;
        }

        public async Task<DeletedContentActorResponse> Handle(DeleteContentActorCommand request, CancellationToken cancellationToken)
        {
            ContentActor? contentActor = await _contentActorRepository.GetAsync(predicate: ca => ca.Id == request.Id, cancellationToken: cancellationToken);
            await _contentActorBusinessRules.ContentActorShouldExistWhenSelected(contentActor);

            await _contentActorRepository.DeleteAsync(contentActor!);

            DeletedContentActorResponse response = _mapper.Map<DeletedContentActorResponse>(contentActor);
            return response;
        }
    }
}