using Core.Application.Responses;

namespace Application.Features.AccountProfiles.Commands.Update;

public class UpdatedAccountProfileResponse : IResponse
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int ProfileId { get; set; }
}