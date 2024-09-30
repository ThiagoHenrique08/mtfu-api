using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoreThanFollowUp.Domain.Entities.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreThanFollowUp.Infrastructure.Configuration.Entities.Projects
{
    internal class PlanningConfiguration : IEntityTypeConfiguration<Planning>
    {
        public void Configure(EntityTypeBuilder<Planning> builder)
        {
            builder.ToTable("Plannings");
            builder.HasKey(p => p.PlanningId);
            builder.Property(p => p.PlanningId).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.DocumentationLink).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.PlanningDescription).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.ProjectId).HasColumnType("INT").IsRequired(false);
            builder.HasOne(p => p.Project).WithOne(c => c.Planning).HasPrincipalKey<Project>(c => c.ProjectId);
        }
    }
}
