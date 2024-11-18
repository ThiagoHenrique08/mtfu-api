using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Application.DTO.Project.Planning
{
    public class POSTPlanningDTO
    {

        public string? DocumentationLink { get; set; }

        public string? PlanningDescription { get; set; }

        public Guid? ProjectId { get; set; }

    }
}
