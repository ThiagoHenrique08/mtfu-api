namespace MoreThanFollowUp.Domain.Entities.Projects
{
    public class Planning : PhaseDefault
    {
        public Guid PlanningId { get; set; }  = Guid.NewGuid();

        public string? DocumentationLink { get; set; }

        public string? PlanningDescription { get; set; }


    }
}
