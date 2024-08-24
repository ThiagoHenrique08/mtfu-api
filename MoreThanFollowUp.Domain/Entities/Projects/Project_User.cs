using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class Project_User : EntityBase
    {
        public int Id { get; set; }
        public virtual Project? Project { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
