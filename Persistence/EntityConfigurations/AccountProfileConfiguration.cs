using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AccountProfileConfiguration : IEntityTypeConfiguration<AccountProfile>
{
    public void Configure(EntityTypeBuilder<AccountProfile> builder)
    {
        builder.ToTable("AccountProfiles").HasKey(ap => ap.Id);

        builder.Property(ap => ap.Id).HasColumnName("Id").IsRequired();
        builder.Property(ap => ap.AccountId).HasColumnName("AccountId");
        builder.Property(ap => ap.ProfileId).HasColumnName("ProfileId");
        builder.Property(ap => ap.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ap => ap.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ap => ap.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ap => !ap.DeletedDate.HasValue);
    }
}