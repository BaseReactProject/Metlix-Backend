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

namespace Application.Features.ContentActors.Commands.Create;

public class CreateContentActorCommand : IRequest<CreatedContentActorResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int ContentId { get; set; }
    public int PersonId { get; set; }

    public string[] Roles => new[] { Admin, Write, ContentActorsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetContentActors";

    public class CreateContentActorCommandHandler : IRequestHandler<CreateContentActorCommand, CreatedContentActorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentActorRepository _contentActorRepository;
        private readonly ContentActorBusinessRules _contentActorBusinessRules;

        public CreateContentActorCommandHandler(IMapper mapper, IContentActorRepository contentActorRepository,
                                         ContentActorBusinessRules contentActorBusinessRules)
        {
            _mapper = mapper;
            _contentActorRepository = contentActorRepository;
            _contentActorBusinessRules = contentActorBusinessRules;
        }

        public async Task<CreatedContentActorResponse> Handle(CreateContentActorCommand request, CancellationToken cancellationToken)
        {
            ContentActor contentActor = _mapper.Map<ContentActor>(request);

            await _contentActorRepository.AddAsync(contentActor);

            CreatedContentActorResponse response = _mapper.Map<CreatedContentActorResponse>(contentActor);
            return response;
        }
    }
}