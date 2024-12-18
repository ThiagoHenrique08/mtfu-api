﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Infrastructure.Configuration.Entities.Projects
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(e => e.ProjectId);
            builder.Property(e => e.ProjectId).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(e => e.Title).HasColumnType("VARCHAR(50)").IsRequired(false);
            builder.Property(e => e.Responsible).HasColumnType("VARCHAR(50)").IsRequired(false);
            builder.Property(e => e.Category).HasColumnType("VARCHAR(50)").IsRequired(false);
            builder.Property(e => e.Status).HasColumnType("VARCHAR(50)").IsRequired(false);
            builder.Property(e => e.Description).HasColumnType("VARCHAR(MAX)").IsRequired(false);
            builder.Property(p => p.CreateDate).HasColumnName("StartDate").HasColumnType("DATETIME").IsRequired(false);
            builder.Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("DATETIME").IsRequired(false);       
            builder.Property(e => e.EnterpriseId).HasColumnType("UNIQUEIDENTIFIER").IsRequired(false);
            builder.HasOne(e => e.Enterprise).WithMany(p => p.Projects).HasPrincipalKey(e => e.EnterpriseId);
        }
    }
}