using MoreThanFollowUp.Domain.Entities.Sprints;

namespace MoreThanFollowUp.Domain.Entities.Projects.Phases
{
    public class RequirementsAnalysisPhase : Period
    {
        public int RequirementsAnalysPhaseId { get; set; }
        //public int ProjectId { get; set; }
        //public virtual Project? Project { get; set; }
        public virtual ICollection<FunctionalRequirements>? FunctionalRequirements { get; set; }
        public virtual ICollection<NotFunctionalRequirements>? NotFunctionalRequirements { get; set; }
        public virtual ICollection<SprintRequirementsAnalysisPhase>? Sprints { get; set; }
    }
}
