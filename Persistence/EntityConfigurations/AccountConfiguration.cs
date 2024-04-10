using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts").HasKey(a => a.Id);

        builder.Property(a => a.Id).HasColumnName("Id").IsRequired();
        builder.Property(a => a.FakeId).HasColumnName("FakeId");
        builder.Property(a => a.UserId).HasColumnName("UserId");
        builder.Property(a => a.PlanId).HasColumnName("PlanId");
        builder.Property(a => a.PhoneNumber).HasColumnName("PhoneNumber");
        builder.Property(a => a.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(a => a.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(a => a.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(a => !a.DeletedDate.HasValue);
        builder.HasData(getSeeds());
    }
    private IEnumerable<Account> getSeeds()
    {
        List<Account> accounts = new();

        Account account = new() { Id = 1, UserId = 2, FakeId = "91356791WIVKOFYCQECMAVVQG",PlanId=2,PhoneNumber="05397726067" };

        accounts.Add(account);

        return accounts;
    }
}