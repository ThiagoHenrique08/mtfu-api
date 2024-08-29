
using Microsoft.AspNetCore.Identity;
using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string? Function { get; set; }

        public string? CompletedName { get; set; }
        public virtual ICollection<Project_User>? Projects_Users { get; set; }
    }
}
