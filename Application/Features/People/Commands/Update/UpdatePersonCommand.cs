using Application.Features.People.Constants;
using Application.Features.People.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.People.Constants.PeopleOperationClaims;

namespace Application.Features.People.Commands.Update;

public class UpdatePersonCommand : IRequest<UpdatedPersonResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, PeopleOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPeople";

    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, UpdatedPersonResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly PersonBusinessRules _personBusinessRules;

        public UpdatePersonCommandHandler(IMapper mapper, IPersonRepository personRepository,
                                         PersonBusinessRules personBusinessRules)
        {
            _mapper = mapper;
            _personRepository = personRepository;
            _personBusinessRules = personBusinessRules;
        }

        public async Task<UpdatedPersonResponse> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            Person? person = await _personRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _personBusinessRules.PersonShouldExistWhenSelected(person);
            person = _mapper.Map(request, person);

            await _personRepository.UpdateAsync(person!);

            UpdatedPersonResponse response = _mapper.Map<UpdatedPersonResponse>(person);
            return response;
        }
    }
}