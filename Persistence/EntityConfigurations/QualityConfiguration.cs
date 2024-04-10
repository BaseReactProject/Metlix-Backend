using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class QualityConfiguration : IEntityTypeConfiguration<Quality>
{
    public void Configure(EntityTypeBuilder<Quality> builder)
    {
        builder.ToTable("Qualities").HasKey(q => q.Id);

        builder.Property(q => q.Id).HasColumnName("Id").IsRequired();
        builder.Property(q => q.Name).HasColumnName("Name");
        builder.Property(q => q.Value).HasColumnName("Value");
        builder.Property(q => q.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(q => q.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(q => q.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(q => !q.DeletedDate.HasValue);
        builder.HasData(getSeeds());
    }
    private IEnumerable<Quality> getSeeds()
    {
        List<Quality> qualities = new ();
        Quality firstQuality = new() { Id = 1, Name = "Ultra HD", Value = 1080 };
        Quality secondQuality = new() { Id = 2, Name = "Full HD", Value = 720 };
        Quality thirdQuality = new() { Id = 3, Name = "Normal Quality", Value = 480 };
        qualities.AddRange(new List<Quality> { firstQuality, secondQuality, thirdQuality });
        return qualities;
    }
}