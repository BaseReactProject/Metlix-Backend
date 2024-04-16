using Application.Features.ContentActors.Constants;
using Application.Features.ContentActors.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ContentActors.Constants.ContentActorsOperationClaims;

namespace Application.Features.ContentActors.Queries.GetById;

public class GetByIdContentActorQuery : IRequest<GetByIdContentActorResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdContentActorQueryHandler : IRequestHandler<GetByIdContentActorQuery, GetByIdContentActorResponse>
    {
        private readonly IMapper _mapper;
        private readonly IContentActorRepository _contentActorRepository;
        private readonly ContentActorBusinessRules _contentActorBusinessRules;

        public GetByIdContentActorQueryHandler(IMapper mapper, IContentActorRepository contentActorRepository, ContentActorBusinessRules contentActorBusinessRules)
        {
            _mapper = mapper;
            _contentActorRepository = contentActorRepository;
            _contentActorBusinessRules = contentActorBusinessRules;
        }

        public async Task<GetByIdContentActorResponse> Handle(GetByIdContentActorQuery request, CancellationToken cancellationToken)
        {
            ContentActor? contentActor = await _contentActorRepository.GetAsync(predicate: ca => ca.Id == request.Id, cancellationToken: cancellationToken);
            await _contentActorBusinessRules.ContentActorShouldExistWhenSelected(contentActor);

            GetByIdContentActorResponse response = _mapper.Map<GetByIdContentActorResponse>(contentActor);
            return response;
        }
    }
}