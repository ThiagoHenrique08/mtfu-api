using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Projects.Phases;

namespace MoreThanFollowUp.Infrastructure.Configuration.Projects.Phases
{
    public class FunctionalRequirementsConfiguration : IEntityTypeConfiguration<FunctionalRequirements>
    {
        public void Configure(EntityTypeBuilder<FunctionalRequirements> builder)
        {
            builder.ToTable("FunctionalRequirements");
            builder.HasKey(p => p.FunctionalRequirementsId);
            builder.Property(p => p.FunctionalRequirementsId).HasColumnType("int").UseIdentityColumn();
            builder.Property(p => p.FunctionOrAction).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.Behavior).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.RequirementsAnalysPhaseId).HasColumnType("int").IsRequired();
            builder.HasOne(p => p.RequirementsAnalysisPhase).WithMany(p => p.FunctionalRequirements).HasPrincipalKey(p => p.RequirementsAnalysPhaseId);
        }
    }
}