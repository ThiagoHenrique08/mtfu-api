using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Infrastructure.Configuration.Models
{
    public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasDiscriminator<string>("Discriminator").HasValue<ApplicationRole>("ApplicationRole");
        }
    }
}