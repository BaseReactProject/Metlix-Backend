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

namespace Application.Features.ContentActors.Commands.Update;

public class UpdateContentActorCommand : IRequest<UpdatedContentActorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public int PersonId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentActorsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentActors";

    public class UpdateContentActorCommandHandler : IRequestHandler<UpdateContentActorCommand, UpdatedContentActorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentActorRepository _contentActorRepository;
        private readonly ContentActorBusinessRules _contentActorBusinessRules;

        public UpdateContentActorCommandHandler(IMapper mapper, IContentActorRepository contentActorRepository,
                                         ContentActorBusinessRules contentActorBusinessRules)
        {
            _mapper = mapper;
            _contentActorRepository = contentActorRepository;
            _contentActorBusinessRules = contentActorBusinessRules;
        }

        public async Task<UpdatedContentActorResponse> Handle(UpdateContentActorCommand request, CancellationToken cancellationToken)
        {
            ContentActor? contentActor = await _contentActorRepository.GetAsync(predicate: ca => ca.Id == request.Id, cancellationToken: cancellationToken);
            await _contentActorBusinessRules.ContentActorShouldExistWhenSelected(contentActor);
            contentActor = _mapper.Map(request, contentActor);

            await _contentActorRepository.UpdateAsync(contentActor!);

            UpdatedContentActorResponse response = _mapper.Map<UpdatedContentActorResponse>(contentActor);
            return response;
        }
    }
}