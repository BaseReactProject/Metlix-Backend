using Core.Application.Responses;

namespace Application.Features.AccountCreditCards.Commands.Delete;

public class DeletedAccountCreditCardResponse : IResponse
{
    public int Id { get; set; }
}