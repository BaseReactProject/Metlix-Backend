using Core.Application.Responses;

namespace Application.Features.Profiles.Commands.Create;

public class CreatedProfileResponse : IResponse
{
    public CreatedProfileResponse()
    {
        Name = string.Empty;
        ImageId = 0;
    }

    public CreatedProfileResponse(string name, int �mageId)
    {

        Name = name;
        ImageId = �mageId;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int ImageId { get; set; }
}