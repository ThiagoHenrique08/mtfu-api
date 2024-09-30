using MoreThanFollowUp.Domain.Entities.Projects;

namespace MoreThanFollowUp.Application.DTO.Project.Planning
{
    public class GETPlanningDTO
    {
        public PlanningDTO? PlanningDTO { get; set; }
        public int? TotalSprint { get; set; }
    }
}
