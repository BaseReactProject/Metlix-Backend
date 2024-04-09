using Core.Application.Responses;

namespace Application.Features.AccountCreditCards.Commands.Create;

public class CreatedAccountCreditCardResponse : IResponse
{
    public int Id { get; set; }
    public int AccountId { get; set; }
}