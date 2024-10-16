using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class Sprint
    {
        public Guid SprintId { get; set; } = Guid.NewGuid();    
        public string? Title { get; set; }

        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }

        public int? SprintScore { get; set; }
        public Guid? PlanningId { get; set; }
        public virtual Planning? Planning { get; set; }

        public Guid? RequirementAnalysisId { get; set; }
        public virtual RequirementAnalysis? RequirementAnalysis { get; set; }

        public virtual ICollection<Sprint_User>? Sprint_Users { get; set; }



    }
}
