using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Infrastructure.Configuration.Entities.Projects
{
    public class RequirementAnalysisConfiguration : IEntityTypeConfiguration<RequirementAnalysis>
    {
        public void Configure(EntityTypeBuilder<RequirementAnalysis> builder)
        {
            builder.ToTable("RequirementAnalysis");
            builder.HasKey(p => p.RequirementAnalysisId);
            builder.Property(p => p.RequirementAnalysisId).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(p => p.StartDate).HasColumnName("Start Date").HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.EndDate).HasColumnName("End Date").HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.ProjectId).HasColumnType("INT").IsRequired(false);
            builder.HasOne(p => p.Project).WithOne(c => c.RequirementAnalysis).HasPrincipalKey<Project>(c => c.ProjectId);
        }
    }
}