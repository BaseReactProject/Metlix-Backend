using Core.Application.Responses;

namespace Application.Features.AccountProfiles.Commands.Create;

public class CreatedAccountProfileResponse : IResponse
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int ProfileId { get; set; }
}