

using Core.Persistence.Repositories;

namespace Domain.Entities;

public class Plan : Entity<int>
{
    public Plan(string name, int qualityId, string description, int deviceCount, decimal price)
    {
        Name = name;
        QualityId = qualityId;
        Description = description;
        DeviceCount = deviceCount;
        Price = price;
    }
    public Plan()
    {
        Name = string.Empty;
        QualityId = 0;
        Description = string.Empty;
        DeviceCount = 0;
        Price = 0;
    }

    public string Name { get; set; }
    public int QualityId { get; set; }
    public string Description { get; set; }
    public int DeviceCount { get; set; }
    public decimal Price { get; set; }
    public virtual Quality? Quality { get; set; }
}
