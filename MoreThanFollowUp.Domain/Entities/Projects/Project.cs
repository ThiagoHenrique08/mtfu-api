using MoreThanFollowUp.Domain.Models;

namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class Project : EntityBase
    {
        public Guid ProjectId { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public string? Responsible { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? EnterpriseId { get; set; }
        public virtual Enterprise? Enterprise { get; set; }

        public virtual Planning? Planning { get; set; }
        public virtual RequirementAnalysis? RequirementAnalysis { get; set; }
        public virtual ICollection<Project_User>? Projects_Users { get; set; }

    }
}
