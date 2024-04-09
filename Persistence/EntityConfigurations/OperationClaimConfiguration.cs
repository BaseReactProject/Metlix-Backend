using Application.Features.OperationClaims.Constants;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasMany(oc => oc.UserOperationClaims);

        builder.HasData(getSeeds());
    }

    private HashSet<OperationClaim> getSeeds()
    {
        int id = 0;
        HashSet<OperationClaim> seeds =
            new()
            {
                new OperationClaim { Id = ++id, Name = GeneralOperationClaims.Admin }
            };
        
        #region Accounts
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Accounts.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Accounts.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Accounts.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Accounts.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Accounts.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Accounts.Delete" });
        
        #endregion
        
        
        #region AccountCreditCards
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "AccountCreditCards.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "AccountCreditCards.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "AccountCreditCards.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "AccountCreditCards.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "AccountCreditCards.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "AccountCreditCards.Delete" });
        
        #endregion
        
        
        #region AccountProfiles
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "AccountProfiles.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "AccountProfiles.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "AccountProfiles.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "AccountProfiles.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "AccountProfiles.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "AccountProfiles.Delete" });
        
        #endregion
        
        
        #region Images
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Images.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Images.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Images.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Images.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Images.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Images.Delete" });
        
        #endregion
        
        
        #region Plans
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Plans.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Plans.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Plans.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Plans.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Plans.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Plans.Delete" });
        
        #endregion
        
        
        #region Profiles
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Profiles.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Profiles.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Profiles.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Profiles.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Profiles.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Profiles.Delete" });
        
        #endregion
        
        
        #region Qualities
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Qualities.Admin" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Qualities.Read" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Qualities.Write" });
        
        seeds.Add(new OperationClaim { Id = ++id, Name = "Qualities.Add" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Qualities.Update" });
        seeds.Add(new OperationClaim { Id = ++id, Name = "Qualities.Delete" });
        
        #endregion
        
        return seeds;
    }
}
