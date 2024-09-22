using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Entities.Resources;

namespace MoreThanFollowUp.Infrastructure.Configuration.Entities.Resources
{
    public class ProjectCategoryConfiguration : IEntityTypeConfiguration<ProjectCategory>
    {
        public void Configure(EntityTypeBuilder<ProjectCategory> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(p => p.CategoryId);
            builder.Property(p => p.CategoryId).HasColumnType("INT").UseIdentityColumn();
            builder.Property(p => p.Name).HasColumnName("Name").HasColumnType("VARCHAR(30)").IsRequired(false);

        }
    }
}
