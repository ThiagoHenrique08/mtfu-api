namespace MoreThanFollowUp.Domain.Models
{
    public class Subscription
    {

        public Guid SubscriptionId { get; set; } = Guid.NewGuid();
        public Guid? TenantId { get; set; }
        public virtual Tenant? Tenant { get; set; }
        public virtual Invoice? Invoice { get; set; }
        public string? Plan { get; set; }// Free, Professional, Enterprise
        public string? Status { get; set; }
        public int? TotalLicense { get; set; }
        public int? TotalAvailable { get; set; }
        public int? TotalUsed { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
