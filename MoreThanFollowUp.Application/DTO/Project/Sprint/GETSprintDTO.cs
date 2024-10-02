namespace MoreThanFollowUp.Application.DTO.Project.Sprint
{
    public class GETSprintDTO
    {
        public int SprintId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Status { get; set; }

    }
}
