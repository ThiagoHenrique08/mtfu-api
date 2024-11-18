using Microsoft.AspNetCore.Identity;

namespace MoreThanFollowUp.Domain.Models
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRoleEnterprise>? Users_Roles_Enteprises { get; set; }
    }
}
