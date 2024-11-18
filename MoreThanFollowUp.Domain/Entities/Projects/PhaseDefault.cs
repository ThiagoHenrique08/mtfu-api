namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class PhaseDefault
    {

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid? ProjectId { get; set; }

        public virtual Project? Project { get; set; }

        public virtual ICollection<Sprint>? Sprints { get; set; }
    }
}
