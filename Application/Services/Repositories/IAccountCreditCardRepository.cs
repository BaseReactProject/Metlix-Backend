using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAccountCreditCardRepository : IAsyncRepository<AccountCreditCard, int>, IRepository<AccountCreditCard, int>
{
}