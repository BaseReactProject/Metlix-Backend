using Core.Application.Responses;

namespace Application.Features.Profiles.Commands.Create;

public class CreatedProfileResponse : IResponse
{
    public CreatedProfileResponse()
    {
        Name = string.Empty;
        ImageId = 0;
    }

    public CreatedProfileResponse(int ýd, string name, int ýmageId)
    {
        Id = ýd;
        Name = name;
        ImageId = ýmageId;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int ImageId { get; set; }
}