using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Infrastructure.Configuration.Models
{
    public class Enteprise_UserConfiguration: IEntityTypeConfiguration<Enterprise_User>
    {
        public void Configure(EntityTypeBuilder<Enterprise_User> builder)
        {
            builder.ToTable("EnterpriseUsers");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(p => p.EnterpriseId).HasColumnType("UNIQUEIDENTIFIER").IsRequired(false);
            builder.HasOne(p => p.Enterprise).WithMany(c => c.Enterprises_Users).HasForeignKey(c => c.EnterpriseId);
            builder.HasOne(p => p.User).WithMany(l => l.Enterprises_Users).HasPrincipalKey(c => c.Id);
        }
    }
}
