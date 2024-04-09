using Core.Application.Responses;

namespace Application.Features.Qualities.Commands.Update;

public class UpdatedQualityResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
}