using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Infrastructure.Configuration.Models
{
    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenants");
            builder.HasKey(t => t.TenantId);
            builder.Property(t=>t.TenantId).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(t => t.TenantName).HasColumnType("VARCHAR(50)").IsRequired();
            builder.Property(t=>t.TenantCustomDomain).HasColumnType("VARCHAR(100)").IsRequired(false);
            builder.Property(t=>t.TenantStatus).HasColumnType("VARCHAR(100)").IsRequired(false);
            builder.Property(t => t.CreatedAt).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(t => t.UpdateAt).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(t => t.Responsible).HasColumnType("VARCHAR(50)").IsRequired(false);
            builder.Property(t => t.Email).HasColumnType("VARCHAR(50)").IsRequired(false);
            builder.Property(t => t.PhoneNumber).HasColumnType("VARCHAR(50)").IsRequired(false);


        }
    }
}
