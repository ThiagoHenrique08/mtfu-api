using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Domain.Models
{
    public class Squad
    {
        public int SquadId { get; set; }

        public string? Name { get; set; }

        public int EnterpriseId { get; set; }

        public virtual Enterprise? Enterprise { get; set; }

        public virtual ICollection<Project>? Projects { get; set; }
    }
}
