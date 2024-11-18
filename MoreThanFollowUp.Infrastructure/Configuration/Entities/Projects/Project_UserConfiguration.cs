using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Infrastructure.Configuration.Entities.Projects
{
    public class Project_UserConfiguration : IEntityTypeConfiguration<Project_User>
    {
        public void Configure(EntityTypeBuilder<Project_User> builder)
        {
            builder.ToTable("ProjectUsers");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(p => p.CreateDate).HasColumnName("DataCriacao").HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.ProjectId).HasColumnType("UNIQUEIDENTIFIER").IsRequired(false);
            builder.HasOne(p => p.Project).WithMany(c => c.Projects_Users).HasForeignKey(c => c.ProjectId);
            builder.HasOne(p => p.User).WithMany(l => l.Projects_Users).HasPrincipalKey(c => c.Id);
        }
    }
}
