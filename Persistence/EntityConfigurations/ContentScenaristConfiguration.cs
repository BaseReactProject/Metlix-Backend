using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ContentScenaristConfiguration : IEntityTypeConfiguration<ContentScenarist>
{
    public void Configure(EntityTypeBuilder<ContentScenarist> builder)
    {
        builder.ToTable("ContentScenarists").HasKey(cs => cs.Id);

        builder.Property(cs => cs.Id).HasColumnName("Id").IsRequired();
        builder.Property(cs => cs.ContentId).HasColumnName("ContentId");
        builder.Property(cs => cs.PersonId).HasColumnName("PersonId");
        builder.Property(cs => cs.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(cs => cs.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(cs => cs.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(cs => !cs.DeletedDate.HasValue);
    }
}