using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class FakeEntityConfiguration : IEntityTypeConfiguration<FakeEntity>
{
    public void Configure(EntityTypeBuilder<FakeEntity> builder)
    {
        builder.ToTable("FakeEntities").HasKey(fe => fe.Id);

        builder.Property(fe => fe.Id).HasColumnName("Id").IsRequired();
        builder.Property(fe => fe.Name).HasColumnName("Name");
        builder.Property(fe => fe.BrandId).HasColumnName("BrandId");
        builder.Property(fe => fe.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(fe => fe.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(fe => fe.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(fe => !fe.DeletedDate.HasValue);
    }
}