using Core.Application.Responses;
using Core.Security.Entities;

namespace Application.Features.Accounts.Commands.Create;

public class CreatedAccountResponse : IResponse
{
    public CreatedAccountResponse()
    {
        FakeId = string.Empty;
        UserId = 0;
    }

    public CreatedAccountResponse(int �d, string fakeId, int userId)
    {
        Id = �d;
        FakeId = fakeId;
        UserId = userId;
    }

    public int Id { get; set; }
    public string FakeId { get; set; }
    public int UserId { get; set; }
}