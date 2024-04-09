using Core.Application.Dtos;

namespace Application.Features.AccountCreditCards.Queries.GetList;

public class GetListAccountCreditCardListItemDto : IDto
{
    public int Id { get; set; }
    public int AccountId { get; set; }
}