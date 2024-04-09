using Application.Features.AccountCreditCards.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.AccountCreditCards.Rules;

public class AccountCreditCardBusinessRules : BaseBusinessRules
{
    private readonly IAccountCreditCardRepository _accountCreditCardRepository;

    public AccountCreditCardBusinessRules(IAccountCreditCardRepository accountCreditCardRepository)
    {
        _accountCreditCardRepository = accountCreditCardRepository;
    }

    public Task AccountCreditCardShouldExistWhenSelected(AccountCreditCard? accountCreditCard)
    {
        if (accountCreditCard == null)
            throw new BusinessException(AccountCreditCardsBusinessMessages.AccountCreditCardNotExists);
        return Task.CompletedTask;
    }

    public async Task AccountCreditCardIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        AccountCreditCard? accountCreditCard = await _accountCreditCardRepository.GetAsync(
            predicate: acc => acc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AccountCreditCardShouldExistWhenSelected(accountCreditCard);
    }
}