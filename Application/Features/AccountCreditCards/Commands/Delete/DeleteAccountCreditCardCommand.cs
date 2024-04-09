using Application.Features.AccountCreditCards.Constants;
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

namespace Application.Features.AccountCreditCards.Commands.Delete;

public class DeleteAccountCreditCardCommand : IRequest<DeletedAccountCreditCardResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, AccountCreditCardsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAccountCreditCards";

    public class DeleteAccountCreditCardCommandHandler : IRequestHandler<DeleteAccountCreditCardCommand, DeletedAccountCreditCardResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccountCreditCardRepository _accountCreditCardRepository;
        private readonly AccountCreditCardBusinessRules _accountCreditCardBusinessRules;

        public DeleteAccountCreditCardCommandHandler(IMapper mapper, IAccountCreditCardRepository accountCreditCardRepository,
                                         AccountCreditCardBusinessRules accountCreditCardBusinessRules)
        {
            _mapper = mapper;
            _accountCreditCardRepository = accountCreditCardRepository;
            _accountCreditCardBusinessRules = accountCreditCardBusinessRules;
        }

        public async Task<DeletedAccountCreditCardResponse> Handle(DeleteAccountCreditCardCommand request, CancellationToken cancellationToken)
        {
            AccountCreditCard? accountCreditCard = await _accountCreditCardRepository.GetAsync(predicate: acc => acc.Id == request.Id, cancellationToken: cancellationToken);
            await _accountCreditCardBusinessRules.AccountCreditCardShouldExistWhenSelected(accountCreditCard);

            await _accountCreditCardRepository.DeleteAsync(accountCreditCard!);

            DeletedAccountCreditCardResponse response = _mapper.Map<DeletedAccountCreditCardResponse>(accountCreditCard);
            return response;
        }
    }
}