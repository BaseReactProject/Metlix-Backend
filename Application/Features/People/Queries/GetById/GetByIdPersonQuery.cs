using Application.Features.People.Constants;
using Application.Features.People.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.People.Constants.PeopleOperationClaims;

namespace Application.Features.People.Queries.GetById;

public class GetByIdPersonQuery : IRequest<GetByIdPersonResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdPersonQueryHandler : IRequestHandler<GetByIdPersonQuery, GetByIdPersonResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        private readonly PersonBusinessRules _personBusinessRules;

        public GetByIdPersonQueryHandler(IMapper mapper, IPersonRepository personRepository, PersonBusinessRules personBusinessRules)
        {
            _mapper = mapper;
            _personRepository = personRepository;
            _personBusinessRules = personBusinessRules;
        }

        public async Task<GetByIdPersonResponse> Handle(GetByIdPersonQuery request, CancellationToken cancellationToken)
        {
            Person? person = await _personRepository.GetAsync(predicate: p => p.Id == request.Id, cancellationToken: cancellationToken);
            await _personBusinessRules.PersonShouldExistWhenSelected(person);

            GetByIdPersonResponse response = _mapper.Map<GetByIdPersonResponse>(person);
            return response;
        }
    }
}