using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AccountCreditCards;

public interface IAccountCreditCardsService
{
    Task<AccountCreditCard?> GetAsync(
        Expression<Func<AccountCreditCard, bool>> predicate,
        Func<IQueryable<AccountCreditCard>, IIncludableQueryable<AccountCreditCard, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<AccountCreditCard>?> GetListAsync(
        Expression<Func<AccountCreditCard, bool>>? predicate = null,
        Func<IQueryable<AccountCreditCard>, IOrderedQueryable<AccountCreditCard>>? orderBy = null,
        Func<IQueryable<AccountCreditCard>, IIncludableQueryable<AccountCreditCard, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<AccountCreditCard> AddAsync(AccountCreditCard accountCreditCard);
    Task<AccountCreditCard> UpdateAsync(AccountCreditCard accountCreditCard);
    Task<AccountCreditCard> DeleteAsync(AccountCreditCard accountCreditCard, bool permanent = false);
}
