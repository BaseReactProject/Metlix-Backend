using Core.Application.Dtos;

namespace Application.Features.Plans.Queries.GetList;

public class GetListPlanListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int QualityId { get; set; }
    public string Description { get; set; }
    public int DeviceCount { get; set; }
    public decimal Price { get; set; }
}