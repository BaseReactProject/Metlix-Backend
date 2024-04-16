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

namespace Application.Features.People.Commands.Create;

public class CreatePersonCommand : IRequest<CreatedPersonResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, PeopleOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPeople";

    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CreatedPersonResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly PersonBusinessRules _personBusinessRules;

        public CreatePersonCommandHandler(IMapper mapper, IPersonRepository personRepository,
                                         PersonBusinessRules personBusinessRules)
        {
            _mapper = mapper;
            _personRepository = personRepository;
            _personBusinessRules = personBusinessRules;
        }

        public async Task<CreatedPersonResponse> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            Person person = _mapper.Map<Person>(request);

            await _personRepository.AddAsync(person);

            CreatedPersonResponse response = _mapper.Map<CreatedPersonResponse>(person);
            return response;
        }
    }
}