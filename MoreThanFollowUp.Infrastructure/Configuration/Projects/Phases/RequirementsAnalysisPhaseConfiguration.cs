using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Entities.Projects.Phases;

namespace MoreThanFollowUp.Infrastructure.Configuration.Projects.Phases
{
    public class RequirementsAnalysisPhaseConfiguration : IEntityTypeConfiguration<RequirementsAnalysisPhase>
    {
        public void Configure(EntityTypeBuilder<RequirementsAnalysisPhase> builder)
        {
            builder.ToTable("RequirementsAnalysis");
            builder.HasKey(p => p.RequirementsAnalysPhaseId);
            builder.Property(p => p.RequirementsAnalysPhaseId).HasColumnType("int").UseIdentityColumn();
            builder.Property(p => p.StartDate).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.EndDate).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.ProjectId).HasColumnType("int").IsRequired();
            builder.HasOne(p => p.Project).WithOne(p => p.RequirementsAnalysPhase).HasPrincipalKey<Project>(p => p.ProjectId);
        }
    }
}
