namespace MoreThanFollowUp.Domain.Entities.Projects.Phases
{
    public class NotFunctionalRequirements
    {

        public int NotFunctionalRequirementsId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int RequirementsAnalysPhaseId { get; set; }
        public virtual RequirementsAnalysisPhase? RequirementsAnalysisPhase { get; set; }
    }
}
