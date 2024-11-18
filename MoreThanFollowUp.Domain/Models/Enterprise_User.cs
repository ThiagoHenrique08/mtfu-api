using System.ComponentModel.DataAnnotations;

namespace MoreThanFollowUp.Domain.Models
{
    public class Enterprise_User
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? EnterpriseId { get; set; }
        public virtual Enterprise? Enterprise { get; set; }
        public virtual ApplicationUser? User { get; set; }
    }
}

