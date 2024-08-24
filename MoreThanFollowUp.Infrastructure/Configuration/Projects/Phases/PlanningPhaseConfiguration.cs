using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Projects;
using MoreThanFollowUp.Domain.Entities.Projects.Phases;

namespace MoreThanFollowUp.Infrastructure.Configuration.Projects.Phases
{
    public class PlanningPhaseConfiguration : IEntityTypeConfiguration<PlanningPhase>
    {
        public void Configure(EntityTypeBuilder<PlanningPhase> builder)
        {
            builder.ToTable("Plannings");
            builder.HasKey(p => p.PlanningPhaseId);
            builder.Property(p => p.PlanningPhaseId).HasColumnType("int").UseIdentityColumn();
            builder.Property(p => p.LinkWebsite).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.Description).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.ProjectId).HasColumnType("INT").IsRequired(false);
            builder.Property(p => p.StartDate).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.EndDate).HasColumnType("DATETIME").IsRequired(false);
            builder.HasOne(p => p.Project).WithOne(p => p.PlanningPhase).HasPrincipalKey<Project>(p => p.ProjectId);
        }
    }
}
