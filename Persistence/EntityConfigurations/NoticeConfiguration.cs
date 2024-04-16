using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class NoticeConfiguration : IEntityTypeConfiguration<Notice>
{
    public void Configure(EntityTypeBuilder<Notice> builder)
    {
        builder.ToTable("Notices").HasKey(n => n.Id);

        builder.Property(n => n.Id).HasColumnName("Id").IsRequired();
        builder.Property(n => n.Name).HasColumnName("Name");
        builder.Property(n => n.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(n => n.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(n => n.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(n => !n.DeletedDate.HasValue);
    }
}