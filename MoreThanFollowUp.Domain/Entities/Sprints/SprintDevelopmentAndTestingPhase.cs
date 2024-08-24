using MoreThanFollowUp.Domain.Entities.Sprints.Tasks;

namespace MoreThanFollowUp.Domain.Entities.Sprints
{
    public class SprintDevelopmentAndTestingPhase : Sprint
    {

        public virtual ICollection<Task_SprintDevelopmentAndTestingPhase>? Task_SprintDevelopmentAndTestingPhase { get; set; }
    }
}
