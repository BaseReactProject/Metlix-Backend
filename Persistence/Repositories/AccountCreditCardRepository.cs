using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AccountCreditCardRepository : EfRepositoryBase<AccountCreditCard, int, BaseDbContext>, IAccountCreditCardRepository
{
    public AccountCreditCardRepository(BaseDbContext context) : base(context)
    {
    }
}