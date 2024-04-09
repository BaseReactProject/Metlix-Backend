using Core.Application.Responses;

namespace Application.Features.AccountCreditCards.Commands.Update;

public class UpdatedAccountCreditCardResponse : IResponse
{
    public int Id { get; set; }
    public int AccountId { get; set; }
}