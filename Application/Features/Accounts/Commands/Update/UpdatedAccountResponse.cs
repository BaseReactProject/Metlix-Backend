using Core.Application.Responses;
using Core.Security.Entities;

namespace Application.Features.Accounts.Commands.Update;

public class UpdatedAccountResponse : IResponse
{
    public UpdatedAccountResponse()
    {
        FakeId = string.Empty;
        UserId = 0;
    }

    public UpdatedAccountResponse(int �d, string fakeId, int userId)
    {
        Id = �d;
        FakeId = fakeId;
        UserId = userId;
    }

    public int Id { get; set; }
    public string FakeId { get; set; }
    public int UserId { get; set; }
}