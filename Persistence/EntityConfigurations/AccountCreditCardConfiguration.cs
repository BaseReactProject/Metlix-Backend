using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AccountCreditCardConfiguration : IEntityTypeConfiguration<AccountCreditCard>
{
    public void Configure(EntityTypeBuilder<AccountCreditCard> builder)
    {
        builder.ToTable("AccountCreditCards").HasKey(acc => acc.Id);

        builder.Property(acc => acc.Id).HasColumnName("Id").IsRequired();
        builder.Property(acc => acc.AccountId).HasColumnName("AccountId");
        builder.Property(acc => acc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(acc => acc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(acc => acc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(acc => !acc.DeletedDate.HasValue);
    }
}