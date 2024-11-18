using Microsoft.AspNetCore.Identity;

namespace MoreThanFollowUp.Domain.Models

{
    public class ApplicationUserRoleEnterprise
    {
        public Guid ApplicationUserRoleEnterpriseId { get; set; } = Guid.NewGuid();
        public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }

        public string? RoleId { get; set; }
        public virtual  ApplicationRole? Role { get; set; }

        public Guid? EnterpriseId { get; set; }
        public virtual Enterprise? Enterprise { get; set; }
    }
}
