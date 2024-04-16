using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ContentGenreConfiguration : IEntityTypeConfiguration<ContentGenre>
{
    public void Configure(EntityTypeBuilder<ContentGenre> builder)
    {
        builder.ToTable("ContentGenres").HasKey(cg => cg.Id);

        builder.Property(cg => cg.Id).HasColumnName("Id").IsRequired();
        builder.Property(cg => cg.ContentId).HasColumnName("ContentId");
        builder.Property(cg => cg.GenreId).HasColumnName("GenreId");
        builder.Property(cg => cg.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(cg => cg.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(cg => cg.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(cg => !cg.DeletedDate.HasValue);
    }
}