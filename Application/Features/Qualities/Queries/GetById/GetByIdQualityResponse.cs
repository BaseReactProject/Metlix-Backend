using Core.Application.Responses;

namespace Application.Features.Qualities.Queries.GetById;

public class GetByIdQualityResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
}