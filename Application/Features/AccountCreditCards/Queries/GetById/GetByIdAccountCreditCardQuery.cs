using Application.Features.AccountCreditCards.Constants;
using Application.Features.AccountCreditCards.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.AccountCreditCards.Constants.AccountCreditCardsOperationClaims;

namespace Application.Features.AccountCreditCards.Queries.GetById;

public class GetByIdAccountCreditCardQuery : IRequest<GetByIdAccountCreditCardResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdAccountCreditCardQueryHandler : IRequestHandler<GetByIdAccountCreditCardQuery, GetByIdAccountCreditCardResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountCreditCardRepository _accountCreditCardRepository;
        private readonly AccountCreditCardBusinessRules _accountCreditCardBusinessRules;

        public GetByIdAccountCreditCardQueryHandler(IMapper mapper, IAccountCreditCardRepository accountCreditCardRepository, AccountCreditCardBusinessRules accountCreditCardBusinessRules)
        {
            _mapper = mapper;
            _accountCreditCardRepository = accountCreditCardRepository;
            _accountCreditCardBusinessRules = accountCreditCardBusinessRules;
        }

        public async Task<GetByIdAccountCreditCardResponse> Handle(GetByIdAccountCreditCardQuery request, CancellationToken cancellationToken)
        {
            AccountCreditCard? accountCreditCard = await _accountCreditCardRepository.GetAsync(predicate: acc => acc.Id == request.Id, cancellationToken: cancellationToken);
            await _accountCreditCardBusinessRules.AccountCreditCardShouldExistWhenSelected(accountCreditCard);

            GetByIdAccountCreditCardResponse response = _mapper.Map<GetByIdAccountCreditCardResponse>(accountCreditCard);
            return response;
        }
    }
}