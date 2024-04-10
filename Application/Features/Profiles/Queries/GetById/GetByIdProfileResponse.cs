using Core.Application.Responses;

namespace Application.Features.Profiles.Queries.GetById;

public class GetByIdProfileResponse : IResponse
{
    public GetByIdProfileResponse()
    {
        Name = string.Empty;
        ImageId = 0;
    }

    public GetByIdProfileResponse(int ýd, string name, int ýmageId)
    {
        Id = ýd;
        Name = name;
        ImageId = ýmageId;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int ImageId { get; set; }
}