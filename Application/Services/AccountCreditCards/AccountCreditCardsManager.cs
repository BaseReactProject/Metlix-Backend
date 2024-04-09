using Application.Features.AccountCreditCards.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AccountCreditCards;

public class AccountCreditCardsManager : IAccountCreditCardsService
{
    private readonly IAccountCreditCardRepository _accountCreditCardRepository;
    private readonly AccountCreditCardBusinessRules _accountCreditCardBusinessRules;

    public AccountCreditCardsManager(IAccountCreditCardRepository accountCreditCardRepository, AccountCreditCardBusinessRules accountCreditCardBusinessRules)
    {
        _accountCreditCardRepository = accountCreditCardRepository;
        _accountCreditCardBusinessRules = accountCreditCardBusinessRules;
    }

    public async Task<AccountCreditCard?> GetAsync(
        Expression<Func<AccountCreditCard, bool>> predicate,
        Func<IQueryable<AccountCreditCard>, IIncludableQueryable<AccountCreditCard, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        AccountCreditCard? accountCreditCard = await _accountCreditCardRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return accountCreditCard;
    }

    public async Task<IPaginate<AccountCreditCard>?> GetListAsync(
        Expression<Func<AccountCreditCard, bool>>? predicate = null,
        Func<IQueryable<AccountCreditCard>, IOrderedQueryable<AccountCreditCard>>? orderBy = null,
        Func<IQueryable<AccountCreditCard>, IIncludableQueryable<AccountCreditCard, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<AccountCreditCard> accountCreditCardList = await _accountCreditCardRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return accountCreditCardList;
    }

    public async Task<AccountCreditCard> AddAsync(AccountCreditCard accountCreditCard)
    {
        AccountCreditCard addedAccountCreditCard = await _accountCreditCardRepository.AddAsync(accountCreditCard);

        return addedAccountCreditCard;
    }

    public async Task<AccountCreditCard> UpdateAsync(AccountCreditCard accountCreditCard)
    {
        AccountCreditCard updatedAccountCreditCard = await _accountCreditCardRepository.UpdateAsync(accountCreditCard);

        return updatedAccountCreditCard;
    }

    public async Task<AccountCreditCard> DeleteAsync(AccountCreditCard accountCreditCard, bool permanent = false)
    {
        AccountCreditCard deletedAccountCreditCard = await _accountCreditCardRepository.DeleteAsync(accountCreditCard);

        return deletedAccountCreditCard;
    }
}
