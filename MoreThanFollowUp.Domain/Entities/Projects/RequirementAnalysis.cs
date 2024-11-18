namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class RequirementAnalysis : PhaseDefault
    {
        public Guid RequirementAnalysisId { get; set; } = Guid.NewGuid();

        public virtual ICollection<DirectOrFunctionalRequirement>? DirectOrFunctionalRequirement { get; set; }

    }
}
