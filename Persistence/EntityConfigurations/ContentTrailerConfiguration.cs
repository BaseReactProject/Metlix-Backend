using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ContentTrailerConfiguration : IEntityTypeConfiguration<ContentTrailer>
{
    public void Configure(EntityTypeBuilder<ContentTrailer> builder)
    {
        builder.ToTable("ContentTrailers").HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id).HasColumnName("Id").IsRequired();
        builder.Property(ct => ct.TrailerId).HasColumnName("TrailerId");
        builder.Property(ct => ct.ContentId).HasColumnName("ContentId");
        builder.Property(ct => ct.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ct => ct.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ct => ct.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ct => !ct.DeletedDate.HasValue);
    }
}