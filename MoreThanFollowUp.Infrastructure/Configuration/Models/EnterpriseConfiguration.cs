using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Infrastructure.Configuration.Models
{
    public class EnterpriseConfiguration : IEntityTypeConfiguration<Enterprise>
    {
        public void Configure(EntityTypeBuilder<Enterprise> builder)
        {
            builder.ToTable("Enterprises");
            builder.HasKey(i => i.EnterpriseId);
            builder.Property(i => i.EnterpriseId).HasColumnType("INT").UseIdentityColumn();
            builder.Property(i => i.TenantId).HasColumnType("INT").IsRequired();
            builder.HasOne(i => i.Tenant).WithMany(i => i.Enterprises).HasForeignKey(t => t.TenantId);
            builder.Property(i => i.CorporateReason).HasColumnType("VARCHAR(100)").IsRequired(false);
            builder.Property(i => i.CNPJ).HasColumnType("VARCHAR(18)").IsRequired(false);
            builder.Property(i => i.Segment).HasColumnType("VARCHAR(100)").IsRequired(false);
        }
    }
}