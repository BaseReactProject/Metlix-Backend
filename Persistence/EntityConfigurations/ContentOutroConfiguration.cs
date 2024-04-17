using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ContentOutroConfiguration : IEntityTypeConfiguration<ContentOutro>
{
    public void Configure(EntityTypeBuilder<ContentOutro> builder)
    {
        builder.ToTable("ContentOutroes").HasKey(co => co.Id);

        builder.Property(co => co.Id).HasColumnName("Id").IsRequired();
        builder.Property(co => co.StartTime).HasColumnName("StartTime");
        builder.Property(co => co.EndTime).HasColumnName("EndTime");
        builder.Property(co => co.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(co => co.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(co => co.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(co => !co.DeletedDate.HasValue);
    }
}