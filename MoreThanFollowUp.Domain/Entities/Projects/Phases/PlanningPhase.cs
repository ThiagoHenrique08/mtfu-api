using MoreThanFollowUp.Domain.Entities.Sprints;

namespace MoreThanFollowUp.Domain.Entities.Projects.Phases
{
    public class PlanningPhase : Period
    {
        public int PlanningPhaseId { get; set; }

        public string? LinkWebsite { get; set; }

        public string? Description { get; set; }

        public int? ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public virtual ICollection<SprintPlanningPhase>? Sprints { get; set; }
    }
}
