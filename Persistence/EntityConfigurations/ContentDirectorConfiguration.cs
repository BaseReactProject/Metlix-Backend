using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ContentDirectorConfiguration : IEntityTypeConfiguration<ContentDirector>
{
    public void Configure(EntityTypeBuilder<ContentDirector> builder)
    {
        builder.ToTable("ContentDirectors").HasKey(cd => cd.Id);

        builder.Property(cd => cd.Id).HasColumnName("Id").IsRequired();
        builder.Property(cd => cd.ContentId).HasColumnName("ContentId");
        builder.Property(cd => cd.PersonId).HasColumnName("PersonId");
        builder.Property(cd => cd.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(cd => cd.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(cd => cd.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(cd => !cd.DeletedDate.HasValue);
    }
}