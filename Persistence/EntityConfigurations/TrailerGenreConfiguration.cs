using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class TrailerGenreConfiguration : IEntityTypeConfiguration<TrailerGenre>
{
    public void Configure(EntityTypeBuilder<TrailerGenre> builder)
    {
        builder.ToTable("TrailerGenres").HasKey(tg => tg.Id);

        builder.Property(tg => tg.Id).HasColumnName("Id").IsRequired();
        builder.Property(tg => tg.TrailerId).HasColumnName("TrailerId");
        builder.Property(tg => tg.GenreId).HasColumnName("GenreId");
        builder.Property(tg => tg.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(tg => tg.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(tg => tg.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(tg => !tg.DeletedDate.HasValue);
    }
}