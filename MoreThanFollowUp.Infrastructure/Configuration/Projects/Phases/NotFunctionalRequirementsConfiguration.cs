using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoreThanFollowUp.Domain.Entities.Projects.Phases;
using MoreThanFollowUp.Domain.Entities.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Infrastructure.Configuration.Projects.Phases
{
    public class NotFunctionalRequirementsConfiguration : IEntityTypeConfiguration<NotFunctionalRequirements>
    {
        public void Configure(EntityTypeBuilder<NotFunctionalRequirements> builder)
        {
            builder.ToTable("NotFunctionalRequirements");
            builder.HasKey(p => p.NotFunctionalRequirementsId);
            builder.Property(p => p.NotFunctionalRequirementsId).HasColumnType("int").UseIdentityColumn();
            builder.Property(p => p.Title).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.Description).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.RequirementsAnalysPhaseId).HasColumnType("int").IsRequired();
            builder.HasOne(p => p.RequirementsAnalysisPhase).WithMany(p => p.NotFunctionalRequirements).HasPrincipalKey(p => p.RequirementsAnalysPhaseId);
        }
    }
}
