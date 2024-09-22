using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Infrastructure.Configuration.Models
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscriptions");
            builder.HasKey(i => i.SubscriptionId);
            builder.Property(i => i.SubscriptionId).HasColumnType("INT").UseIdentityColumn();
            builder.Property(i => i.TenantId).HasColumnType("INT").IsRequired();
            builder.HasOne(i => i.Tenant).WithOne(i => i.Subscription).HasPrincipalKey<Tenant>(t => t.TenantId);
            builder.Property(i => i.Plan).HasColumnType("VARCHAR(30)").IsRequired(false);
            builder.Property(i => i.Status).HasColumnType("VARCHAR(30)").IsRequired(false);
            builder.Property(i => i.StartDate).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(i => i.EndDate).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(i => i.TotalLicense).HasColumnType("INT").IsRequired(false);
            builder.Property(i => i.TotalAvailable).HasColumnType("INT").IsRequired(false);
            builder.Property(i => i.TotalUsed).HasColumnType("INT").IsRequired(false);
        }
    }
}
