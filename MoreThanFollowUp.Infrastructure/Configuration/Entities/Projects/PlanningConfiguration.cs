using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Infrastructure.Configuration.Entities.Projects
{
    internal class PlanningConfiguration : IEntityTypeConfiguration<Planning>
    {
        public void Configure(EntityTypeBuilder<Planning> builder)
        {
            builder.ToTable("Plannings");
            builder.HasKey(p => p.PlanningId);
            builder.Property(p => p.PlanningId).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(p => p.DocumentationLink).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.PlanningDescription).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.StartDate).HasColumnName("Start Date").HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.EndDate).HasColumnName("End Date").HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.ProjectId).HasColumnType("INT").IsRequired(false);
            builder.HasOne(p => p.Project).WithOne(c => c.Planning).HasPrincipalKey<Project>(c => c.ProjectId);
        }
    }
}
