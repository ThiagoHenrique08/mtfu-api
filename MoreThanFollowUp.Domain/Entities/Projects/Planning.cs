namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class Planning
    {
        public int PlanningId { get; set; }
        public string? DocumentationLink { get; set; }

        public string? PlanningDescription { get; set; }

        public int? ProjectId { get; set; }

        public virtual Project? Project { get; set; }

        public virtual ICollection<Sprint>? Sprints { get; set; }

    }
}
