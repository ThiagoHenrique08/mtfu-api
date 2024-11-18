using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Domain.Models
{
    public class Enterprise
    {
        public Guid EnterpriseId { get; set; } = Guid.NewGuid();
        public string? CorporateReason { get; set; }
        public string? CNPJ { get; set; }
        public string? Segment { get; set; }
        public Guid? TenantId { get; set; }
        public virtual Tenant? Tenant { get; set; }
        public virtual ICollection<Project>? Projects { get; set; }
        public virtual ICollection<Enterprise_User>? Enterprises_Users { get; set; }
        public virtual ICollection<ApplicationUserRoleEnterprise>? Users_Roles_Enteprises { get; set; }

    }
}
