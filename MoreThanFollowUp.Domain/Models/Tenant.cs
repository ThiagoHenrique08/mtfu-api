namespace MoreThanFollowUp.Domain.Models
{
    public class Tenant
    {
        public int TenantId { get; set; }
        public string? TenantName { get; set; }
        public string? TenantCustomDomain { get; set; }
        public string? TenantStatus { get; set; }
        public string? Responsible { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public virtual Subscription? Subscription { get; set; }
        public virtual ICollection<Enterprise>? Enterprises { get; set; }

    }

}
