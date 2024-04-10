using Core.Application.Responses;

namespace Application.Features.Qualities.Queries.GetById;

public class GetByIdQualityResponse : IResponse
{
    public GetByIdQualityResponse()
    {
        Name = string.Empty;
        Value = 0;
    }

    public GetByIdQualityResponse(int ýd, string name, int value)
    {
        Id = ýd;
        Name = name;
        Value = value;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
}