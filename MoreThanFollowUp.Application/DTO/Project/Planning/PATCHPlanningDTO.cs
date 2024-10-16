namespace MoreThanFollowUp.Application.DTO.Project.Planning
{
    public class PATCHPlanningDTO
    {
        public Guid? PhaseId { get; set; }
        public string? DocumentationLink { get; set; }

        public string? PlanningDescription { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
