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

namespace Application.Features.People.Commands.Delete;

public class DeletePersonCommand : IRequest<DeletedPersonResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, PeopleOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetPeople";

    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, DeletedPersonResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly PersonBusinessRules _personBusinessRules;

        public DeletePersonCommandHandler(IMapper mapper, IPersonRepository personRepository,
                                         PersonBusinessRules personBusinessRules)
        {
            _mapper = mapper;
            _personRepository = personRepository;
            _personBusinessRules = personBusinessRules;
        }

        public async Task<DeletedPersonResponse> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            Person? person = await _personRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _personBusinessRules.PersonShouldExistWhenSelected(person);

            await _personRepository.DeleteAsync(person!);

            DeletedPersonResponse response = _mapper.Map<DeletedPersonResponse>(person);
            return response;
        }
    }
}