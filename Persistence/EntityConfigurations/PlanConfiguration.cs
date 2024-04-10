using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.ToTable("Plans").HasKey(p => p.Id);

        builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
        builder.Property(p => p.Name).HasColumnName("Name");
        builder.Property(p => p.QualityId).HasColumnName("QualityId");
        builder.Property(p => p.Description).HasColumnName("Description");
        builder.Property(p => p.DeviceCount).HasColumnName("DeviceCount");
        builder.Property(p => p.Price).HasColumnName("Price");
        builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(p => !p.DeletedDate.HasValue);
        builder.HasData(getSeeds());
    }
    private IEnumerable<Plan> getSeeds()
    {
        List<Plan> plans = new()
        {
            new Plan()
            {
                Id=1,
                Name="Standart Plan",
                DeviceCount=1,
                Price=0,
                QualityId=3
            },
            new Plan()
            {
                Id=2,
                Name="Full Plan",
                DeviceCount=4,
                Price=10,
                QualityId=2
            },
            new Plan()
            {
                Id=3,
                Name="Ultra Plan",
                DeviceCount=6,
                Price=20,
                QualityId=1
            },
        };
        return plans;
    }
}