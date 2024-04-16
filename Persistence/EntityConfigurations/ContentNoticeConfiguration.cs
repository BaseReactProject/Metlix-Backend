using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ContentNoticeConfiguration : IEntityTypeConfiguration<ContentNotice>
{
    public void Configure(EntityTypeBuilder<ContentNotice> builder)
    {
        builder.ToTable("ContentNotices").HasKey(cn => cn.Id);

        builder.Property(cn => cn.Id).HasColumnName("Id").IsRequired();
        builder.Property(cn => cn.ContentId).HasColumnName("ContentId");
        builder.Property(cn => cn.NoticeId).HasColumnName("NoticeId");
        builder.Property(cn => cn.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(cn => cn.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(cn => cn.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(cn => !cn.DeletedDate.HasValue);
    }
}