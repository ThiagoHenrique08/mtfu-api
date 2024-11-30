using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Infrastructure.Configuration.Models
{
    public class ApplicationUserRoleEnterpriseTenantConfiguration : IEntityTypeConfiguration<ApplicationUserRoleEnterpriseTenant>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRoleEnterpriseTenant> builder)
        {
            builder.ToTable("ApplicationUserRoleEnterprisesTenants");
            builder.HasKey(e => e.Id);
            builder.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(p => p.EnterpriseId).HasColumnType("UNIQUEIDENTIFIER").IsRequired(false);
            builder.HasOne(p => p.Enterprise).WithMany(c => c.Users_Roles_Enteprises_Tenants).HasPrincipalKey(c => c.EnterpriseId);
            builder.Property(p => p.RoleId).HasColumnName("RoleId").IsRequired(false);
            builder.HasOne(p => p.Role).WithMany(c => c.Users_Roles_Enteprises_Tenants).HasPrincipalKey(c => c.Id);
            builder.Property(p => p.UserId).HasColumnName("UserId").IsRequired(false);
            builder.HasOne(p => p.User).WithMany(c => c.Users_Roles_Enteprises_Tenants).HasPrincipalKey(c => c.Id);
            builder.Property(p => p.TenantId).HasColumnType("UNIQUEIDENTIFIER").IsRequired(false);
            builder.HasOne(p => p.Tenant).WithMany(c => c.Users_Roles_Enteprises_Tenants).HasPrincipalKey(c => c.TenantId);
        }
    }
}