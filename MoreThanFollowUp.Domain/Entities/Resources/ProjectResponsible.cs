namespace MoreThanFollowUp.Domain.Entities.Resources
{
    public class ProjectResponsible
    {
        public Guid ResponsibleId { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
    }
}
