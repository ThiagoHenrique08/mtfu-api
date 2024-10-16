using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Infrastructure.Configuration.Entities.Projects
{
    public class SprintConfiguration : IEntityTypeConfiguration<Sprint>
    {
        public void Configure(EntityTypeBuilder<Sprint> builder)
        {
            builder.ToTable("Sprints");
            builder.HasKey(p => p.SprintId);
            builder.Property(p => p.SprintId).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(p => p.Title).HasColumnName("Link").HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.Description).HasColumnName("Description").HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.Status).HasColumnName("Status").HasColumnType("VARCHAR(20)").IsRequired(false);
            builder.Property(p => p.StartDate).HasColumnName("Start Date").HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.EndDate).HasColumnName("End Date").HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.SprintScore).HasColumnType("INT").IsRequired(false);
            builder.Property(p => p.PlanningId).HasColumnType("UNIQUEIDENTIFIER").IsRequired(false);
            // Relacionamento com Planning
            builder.HasOne(p => p.Planning)
                   .WithMany(c => c.Sprints)
                   .HasForeignKey(p => p.PlanningId) // Usando PhaseId como chave estrangeira
                   .HasPrincipalKey(c => c.PlanningId)
                   .OnDelete(DeleteBehavior.Restrict).IsRequired(false); ; // Impede deleção em cascata


            builder.Property(p => p.RequirementAnalysisId).HasColumnType("UNIQUEIDENTIFIER").IsRequired(false);
            // Relacionamento com RequirementAnalysis
            builder.HasOne(p => p.RequirementAnalysis)
                   .WithMany(c => c.Sprints)
                   .HasForeignKey(p => p.RequirementAnalysisId) // Usando PhaseId como chave estrangeira
                   .HasPrincipalKey(c => c.RequirementAnalysisId)
                   .OnDelete(DeleteBehavior.Restrict).IsRequired(false);  // Impede deleção em cascata
        }
    }
}