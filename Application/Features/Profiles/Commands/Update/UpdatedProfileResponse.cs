using Core.Application.Responses;

namespace Application.Features.Profiles.Commands.Update;

public class UpdatedProfileResponse : IResponse
{
    public UpdatedProfileResponse()
    {
        Name = string.Empty;
        ImageId = 0;
    }

    public UpdatedProfileResponse(string name, int ýmageId)
    {
        Name = name;
        ImageId = ýmageId;
    }
    public string Name { get; set; }
    public int ImageId { get; set; }
}