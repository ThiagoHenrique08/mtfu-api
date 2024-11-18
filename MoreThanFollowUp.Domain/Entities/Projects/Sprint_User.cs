using MoreThanFollowUp.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class Sprint_User : EntityBase
    {
         
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? SprintId { get; set; }
        public virtual Sprint? Sprint { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}
