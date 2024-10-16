namespace MoreThanFollowUp.Application.DTO.Project.Planning
{
    public class GETPlanningDTO
    {
        public Guid? PlanningId { get; set; }
        public string? DocumentationLink { get; set; }
        public string? PlanningDescription { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
