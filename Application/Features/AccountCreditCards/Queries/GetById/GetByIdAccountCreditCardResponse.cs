using Core.Application.Responses;

namespace Application.Features.AccountCreditCards.Queries.GetById;

public class GetByIdAccountCreditCardResponse : IResponse
{
    public int Id { get; set; }
    public int AccountId { get; set; }
}