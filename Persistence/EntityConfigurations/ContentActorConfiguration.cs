using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ContentActorConfiguration : IEntityTypeConfiguration<ContentActor>
{
    public void Configure(EntityTypeBuilder<ContentActor> builder)
    {
        builder.ToTable("ContentActors").HasKey(ca => ca.Id);

        builder.Property(ca => ca.Id).HasColumnName("Id").IsRequired();
        builder.Property(ca => ca.ContentId).HasColumnName("ContentId");
        builder.Property(ca => ca.PersonId).HasColumnName("PersonId");
        builder.Property(ca => ca.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ca => ca.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ca => ca.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ca => !ca.DeletedDate.HasValue);
    }
}