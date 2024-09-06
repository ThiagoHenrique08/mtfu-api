using MoreThanFollowUp.Domain.Entities.Projects.Phases;

namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class Project : EntityBase
    {
        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Responsible { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual ICollection<Project_User>? Projects_Users { get; set; }

        public virtual PlanningPhase? PlanningPhase { get; set; }
        public virtual RequirementsAnalysisPhase? RequirementsAnalysPhase { get; set; }
    }
}
