using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Infrastructure.Configuration.Models
{
    public class ApplicationUserRoleEnterpriseConfiguration : IEntityTypeConfiguration<ApplicationUserRoleEnterprise>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRoleEnterprise> builder)
        {
            builder.ToTable("ApplicationUserRoleEnterprises");
            builder.HasKey(e => e.ApplicationUserRoleEnterpriseId);
            builder.Property(p => p.ApplicationUserRoleEnterpriseId).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(p => p.EnterpriseId).HasColumnType("UNIQUEIDENTIFIER").IsRequired(false);
            builder.HasOne(p => p.Enterprise).WithMany(c => c.Users_Roles_Enteprises).HasPrincipalKey(c => c.EnterpriseId);
            builder.Property(p => p.RoleId).HasColumnName("RoleId").IsRequired(false);
            builder.HasOne(p => p.Role).WithMany(c => c.Users_Roles_Enteprises).HasPrincipalKey(c => c.Id);
            builder.Property(p => p.UserId).HasColumnName("UserId").IsRequired(false);
            builder.HasOne(p => p.User).WithMany(c => c.Users_Roles_Enteprises).HasPrincipalKey(c => c.Id);
        }
    }
}