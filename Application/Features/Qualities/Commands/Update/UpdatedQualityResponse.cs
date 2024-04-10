using Core.Application.Responses;
namespace Application.Features.Qualities.Commands.Update;

public class UpdatedQualityResponse : IResponse
{
    public UpdatedQualityResponse()
    {
        Name = string.Empty;
        Value = 0;
    }

    public UpdatedQualityResponse(int �d, string name, int value)
    {
        Id = �d;
        Name = name;
        Value = value;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
}