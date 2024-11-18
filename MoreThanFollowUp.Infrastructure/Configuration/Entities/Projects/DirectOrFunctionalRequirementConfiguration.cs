using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Infrastructure.Configuration.Entities.Projects
{
    public class DirectOrFunctionalRequirementConfiguration : IEntityTypeConfiguration<DirectOrFunctionalRequirement>
    {
        public void Configure(EntityTypeBuilder<DirectOrFunctionalRequirement> builder)
        {
            builder.ToTable("DirectOrFunctionalRequirements");
            builder.HasKey(p => p.DirectOrFunctionalRequirementId);
            builder.Property(p=>p.DirectOrFunctionalRequirementId).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(p => p.FunctionOrAction).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.SystemBehavior).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p=>p.RequirementAnalysisId).HasColumnType("UNIQUEIDENTIFIER").IsRequired(false);
            builder.HasOne(p => p.RequirementAnalysis).WithMany(p =>p.DirectOrFunctionalRequirement).HasForeignKey(p => p.RequirementAnalysisId);
        }
    }
}
