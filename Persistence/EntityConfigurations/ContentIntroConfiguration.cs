using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ContentIntroConfiguration : IEntityTypeConfiguration<ContentIntro>
{
    public void Configure(EntityTypeBuilder<ContentIntro> builder)
    {
        builder.ToTable("ContentIntroes").HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id).HasColumnName("Id").IsRequired();
        builder.Property(ci => ci.StartTime).HasColumnName("StartTime");
        builder.Property(ci => ci.EndTime).HasColumnName("EndTime");
        builder.Property(ci => ci.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ci => ci.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ci => ci.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ci => !ci.DeletedDate.HasValue);
    }
}