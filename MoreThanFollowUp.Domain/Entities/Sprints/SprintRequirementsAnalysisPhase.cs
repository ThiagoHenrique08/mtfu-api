using MoreThanFollowUp.Domain.Entities.Projects.Phases;

namespace MoreThanFollowUp.Domain.Entities.Sprints
{
    public class SprintRequirementsAnalysisPhase : Sprint
    {
        public int RequirementsAnalysPhaseId { get; set; }
        public virtual RequirementsAnalysisPhase? RequirementsAnalysisPhase { get; set; }

        //public virtual ICollection<Task_SprintRequirementsAnalysisPhase>? Task_SprintRequirementsAnalysisPhase { get; set; }
    }
}
