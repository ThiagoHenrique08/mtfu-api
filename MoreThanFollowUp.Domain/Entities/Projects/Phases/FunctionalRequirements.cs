namespace MoreThanFollowUp.Domain.Entities.Projects.Phases
{
    public class FunctionalRequirements
    {
        public int FunctionalRequirementsId { get; set; }

        public string? FunctionOrAction { get; set; }

        public string? Behavior { get; set; }

        public int RequirementsAnalysPhaseId { get; set; }
        public virtual RequirementsAnalysisPhase? RequirementsAnalysisPhase { get; set; }
    }
}
