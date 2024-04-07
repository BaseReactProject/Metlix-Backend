using Core.Application.Responses;

namespace Application.Features.Accounts.Commands.Update;

public class UpdatedAccountResponse : IResponse
{
    public int Id { get; set; }
    public string FakeId { get; set; }
    public int UserId { get; set; }
}