using Core.Application.Responses;

namespace Application.Features.Profiles.Commands.Update;

public class UpdatedProfileResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ImageId { get; set; }
}