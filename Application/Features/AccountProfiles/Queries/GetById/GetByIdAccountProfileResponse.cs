using Core.Application.Responses;

namespace Application.Features.AccountProfiles.Queries.GetById;

public class GetByIdAccountProfileResponse : IResponse
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int ProfileId { get; set; }
}