using Application.Features.AccountCreditCards.Constants;
using Application.Features.AccountCreditCards.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.AccountCreditCards.Constants.AccountCreditCardsOperationClaims;

namespace Application.Features.AccountCreditCards.Commands.Update;

public class UpdateAccountCreditCardCommand : IRequest<UpdatedAccountCreditCardResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int AccountId { get; set; }

    public string[] Roles => new[] { Admin, Write, AccountCreditCardsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAccountCreditCards";

    public class UpdateAccountCreditCardCommandHandler : IRequestHandler<UpdateAccountCreditCardCommand, UpdatedAccountCreditCardResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountCreditCardRepository _accountCreditCardRepository;
        private readonly AccountCreditCardBusinessRules _accountCreditCardBusinessRules;

        public UpdateAccountCreditCardCommandHandler(IMapper mapper, IAccountCreditCardRepository accountCreditCardRepository,
                                         AccountCreditCardBusinessRules accountCreditCardBusinessRules)
        {
            _mapper = mapper;
            _accountCreditCardRepository = accountCreditCardRepository;
            _accountCreditCardBusinessRules = accountCreditCardBusinessRules;
        }

        public async Task<UpdatedAccountCreditCardResponse> Handle(UpdateAccountCreditCardCommand request, CancellationToken cancellationToken)
        {
            AccountCreditCard? accountCreditCard = await _accountCreditCardRepository.GetAsync(predicate: acc => acc.Id == request.Id, cancellationToken: cancellationToken);
            await _accountCreditCardBusinessRules.AccountCreditCardShouldExistWhenSelected(accountCreditCard);
            accountCreditCard = _mapper.Map(request, accountCreditCard);

            await _accountCreditCardRepository.UpdateAsync(accountCreditCard!);

            UpdatedAccountCreditCardResponse response = _mapper.Map<UpdatedAccountCreditCardResponse>(accountCreditCard);
            return response;
        }
    }
}