using Core.Application.Responses;
using Core.Security.Entities;

namespace Application.Features.Accounts.Queries.GetById;

public class GetByIdAccountResponse : IResponse
{
    public GetByIdAccountResponse()
    {
        FakeId = string.Empty;
        UserId = 0;
    }

    public GetByIdAccountResponse(int id, string fakeId, int userId)
    {
        Id = id;
        FakeId = fakeId;
        UserId = userId;
    }

    public int Id { get; set; }
    public string FakeId { get; set; }
    public int UserId { get; set; }
}