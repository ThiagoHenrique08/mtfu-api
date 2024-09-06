using MoreThanFollowUp.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class Project_User : EntityBase
    {
        [Key]
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        //public string? UserId { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
