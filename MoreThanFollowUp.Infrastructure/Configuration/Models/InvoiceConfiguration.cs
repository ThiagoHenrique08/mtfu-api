using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Infrastructure.Configuration.Models
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");
            builder.HasKey(i=>i.InvoiceId);
            builder.Property(i=>i.InvoiceId).HasColumnType("UNIQUEIDENTIFIER").ValueGeneratedOnAdd();
            builder.Property(i=>i.Amount).HasColumnType("DECIMAL(10,2)").IsRequired(false);
            builder.Property(i => i.Status).HasColumnType("VARCHAR(30)").IsRequired(false);
            builder.Property(i =>i.CreateAt).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(i => i.DueDate).HasColumnType("DATETIME").IsRequired(false);
            builder.Property(i => i.SubscriptionId).HasColumnType("UNIQUEIDENTIFIER").IsRequired(false);
            builder.HasOne(i => i.Subscription).WithOne(i => i.Invoice).HasPrincipalKey<Subscription>(t => t.SubscriptionId);

        }
    }
}
