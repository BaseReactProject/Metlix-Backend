using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ContentConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.ToTable("Contents").HasKey(c => c.Id);

        builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
        builder.Property(c => c.Name).HasColumnName("Name");
        builder.Property(c => c.MovieId).HasColumnName("MovieId");
        builder.Property(c => c.ThumbnailUrl).HasColumnName("ThumbnailUrl");
        builder.Property(c => c.Duration).HasColumnName("Duration");
        builder.Property(c => c.ReleaseDate).HasColumnName("ReleaseDate");
        builder.Property(c => c.AgeLimit).HasColumnName("AgeLimit");
        builder.Property(c => c.Description).HasColumnName("Description");
        builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(c => !c.DeletedDate.HasValue);
    }
}