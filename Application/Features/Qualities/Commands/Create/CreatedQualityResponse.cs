using Core.Application.Responses;

namespace Application.Features.Qualities.Commands.Create;

public class CreatedQualityResponse : IResponse
{
    public CreatedQualityResponse()
    {
        Name = string.Empty;
        Value = 0;
    }

    public CreatedQualityResponse(int �d, string name, int value)
    {
        Id = �d;
        Name = name;
        Value = value;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
}