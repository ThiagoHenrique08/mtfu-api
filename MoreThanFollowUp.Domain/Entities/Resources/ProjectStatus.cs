namespace MoreThanFollowUp.Domain.Entities.Resources
{
    public class ProjectStatus
    {
        public Guid  StatusProjectId { get; set; } = Guid.NewGuid();    
        public string? Name { get; set; }


    }
}
