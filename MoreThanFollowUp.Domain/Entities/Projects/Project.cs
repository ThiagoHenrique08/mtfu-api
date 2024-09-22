using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class Project : EntityBase
    {
        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string? Responsible { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTime? EndDate { get; set; }
        public int? EnterpriseId { get; set; }
        public virtual Enterprise? Enterprise { get; set; }
        public virtual ICollection<Project_User>? Projects_Users { get; set; }

    }
}
