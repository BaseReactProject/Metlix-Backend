using Core.Application.Responses;
using Domain.Entities;
using Nest;

namespace Application.Features.Plans.Commands.Update;

public class UpdatedPlanResponse : Core.Application.Responses.IResponse
{
    public UpdatedPlanResponse()
    {
        Name = string.Empty;
        QualityId = 0;
        Description = string.Empty;
        DeviceCount = 0;
        Price = 0;
    }

    public UpdatedPlanResponse(int id, string name, int qualityId, string description, int deviceCount, decimal price)
    {
        Id = id;
        Name = name;
        QualityId = qualityId;
        Description = description;
        DeviceCount = deviceCount;
        Price = price;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int QualityId { get; set; }
    public string Description { get; set; }
    public int DeviceCount { get; set; }
    public decimal Price { get; set; }
}