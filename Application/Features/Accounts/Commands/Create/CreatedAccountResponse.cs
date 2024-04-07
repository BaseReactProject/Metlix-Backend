using Core.Application.Responses;

namespace Application.Features.Accounts.Commands.Create;

public class CreatedAccountResponse : IResponse
{
    public int Id { get; set; }
    public string FakeId { get; set; }
    public int UserId { get; set; }
}