using Core.Application.Responses;

namespace Application.Features.Profiles.Commands.Create;

public class CreatedProfileResponse : IResponse
{
    public CreatedProfileResponse()
    {
        Name = string.Empty;
        ImageId = 0;
    }

    public CreatedProfileResponse(int �d, string name, int �mageId)
    {
        Id = �d;
        Name = name;
        ImageId = �mageId;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int ImageId { get; set; }
}