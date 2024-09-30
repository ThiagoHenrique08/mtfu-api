namespace MoreThanFollowUp.Application.DTO.Project.Sprint
{
    public class POSTSprintDTO
    {
        public string? Title { get; set; }

        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }
        public int? PlanningId { get; set; }

    }
}
