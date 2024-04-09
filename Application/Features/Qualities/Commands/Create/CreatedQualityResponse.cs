using Core.Application.Responses;

namespace Application.Features.Qualities.Commands.Create;

public class CreatedQualityResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
}