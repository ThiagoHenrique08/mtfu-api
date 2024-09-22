using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Domain.Models
{
    public class Enterprise
    {
        public int EnterpriseId { get; set; }
        public string? CorporateReason { get; set; }
        public string? CNPJ { get; set; }
        public string? Segment { get; set; }
        public int TenantId { get; set; }
        public virtual Tenant? Tenant { get; set; }
        public virtual ICollection<Project>? Projects { get; set; }
        public virtual ICollection<ApplicationUser>? Users { get; set; }

    }
}
