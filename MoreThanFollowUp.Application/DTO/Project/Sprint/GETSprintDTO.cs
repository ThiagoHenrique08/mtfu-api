using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Application.DTO.Project.Sprint
{
    public class GETSprintDTO
    {
        public Guid SprintId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? SprintScorte {  get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Status { get; set; }

        public virtual ICollection<string>? Sprint_Users { get; set; }

    }
}
