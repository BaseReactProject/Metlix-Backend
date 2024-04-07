using Core.Application.Responses;

namespace Application.Features.Accounts.Queries.GetById;

public class GetByIdAccountResponse : IResponse
{
    public int Id { get; set; }
    public string FakeId { get; set; }
    public int UserId { get; set; }
}