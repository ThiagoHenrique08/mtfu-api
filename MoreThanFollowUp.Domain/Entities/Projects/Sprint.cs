using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class Sprint
    {
        public int SprintId { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }

        public int? SprintScore { get; set; }
        public int? PlanningId { get; set; }
        public virtual Planning? Planning { get; set; }

        public virtual ICollection<Sprint_User>? Sprint_Users { get; set; }



    }
}
