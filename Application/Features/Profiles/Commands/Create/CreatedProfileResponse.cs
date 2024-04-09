using Core.Application.Responses;

namespace Application.Features.Profiles.Commands.Create;

public class CreatedProfileResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ImageId { get; set; }
}