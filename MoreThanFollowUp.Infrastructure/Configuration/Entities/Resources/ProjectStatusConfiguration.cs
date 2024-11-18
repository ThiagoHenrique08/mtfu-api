using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Resources;

namespace MoreThanFollowUp.Infrastructure.Configuration.Entities.Resources
{
    public class ProjectStatusConfiguration : IEntityTypeConfiguration<ProjectStatus>
    {
        public void Configure(EntityTypeBuilder<ProjectStatus> builder)
        {
            builder.ToTable("ProjectStatus");
            builder.HasKey(p => p.StatusProjectId);
            builder.Property(p => p.StatusProjectId).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasColumnName("Name").HasColumnType("VARCHAR(30)").IsRequired();
        }
    }
}
