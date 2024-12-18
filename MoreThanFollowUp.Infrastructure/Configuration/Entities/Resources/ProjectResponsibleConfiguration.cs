﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Resources;

namespace MoreThanFollowUp.Infrastructure.Configuration.Entities.Resources
{
    public class ProjectResponsibleConfiguration : IEntityTypeConfiguration<ProjectResponsible>
    {
        public void Configure(EntityTypeBuilder<ProjectResponsible> builder)
        {
            builder.ToTable("Responsible");
            builder.HasKey(p => p.ResponsibleId);
            builder.Property(p => p.ResponsibleId).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasColumnName("Name").HasColumnType("VARCHAR(30)").IsRequired();
        }
    }
}
