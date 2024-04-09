using Core.Application.Responses;

namespace Application.Features.Plans.Commands.Update;

public class UpdatedPlanResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int QualityId { get; set; }
    public string Description { get; set; }
    public int DeviceCount { get; set; }
    public decimal Price { get; set; }
}