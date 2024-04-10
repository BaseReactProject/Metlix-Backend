using Core.Application.Responses;

namespace Application.Features.Profiles.Commands.Update;

public class UpdatedProfileResponse : IResponse
{
    public UpdatedProfileResponse()
    {
        Name = string.Empty;
        ImageId = 0;
    }

    public UpdatedProfileResponse(int �d, string name, int �mageId)
    {
        Id = �d;
        Name = name;
        ImageId = �mageId;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int ImageId { get; set; }
}