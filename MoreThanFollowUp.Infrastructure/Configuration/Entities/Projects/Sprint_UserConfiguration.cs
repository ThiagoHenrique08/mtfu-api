using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Infrastructure.Configuration.Entities.Projects
{
    public class Sprint_UserConfiguration : IEntityTypeConfiguration<Sprint_User>
    {
        public void Configure(EntityTypeBuilder<Sprint_User> builder)
        {
            builder.ToTable("SprintUsers");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.CreateDate).HasColumnName("DataCriacao").HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.SprintId).HasColumnType("INT").IsRequired(false);
            builder.HasOne(p => p.Sprint).WithMany(c => c.Sprint_Users).HasForeignKey(c => c.SprintId);
            builder.HasOne(p => p.User).WithMany(l => l.Sprint_Users).HasPrincipalKey(c => c.Id);
        }
    }
}