using System.ComponentModel.DataAnnotations;

namespace MoreThanFollowUp.Domain.Entities.Sprints
{
    public class Sprint
    {
        [Key]
        public int SprintId { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
