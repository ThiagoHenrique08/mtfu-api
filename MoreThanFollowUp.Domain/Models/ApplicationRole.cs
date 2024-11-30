using Microsoft.AspNetCore.Identity;

namespace MoreThanFollowUp.Domain.Models
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRoleEnterpriseTenant>? Users_Roles_Enteprises_Tenants { get; set; }
    }
}
